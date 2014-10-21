using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace EdiQuery.Extensions
{
    public class InequalityResolver : IResolveOperators
    {
        public bool CanProcess(ExpressionType expressionType)
        {
            return expressionType == ExpressionType.NotEqual;
        }

        public string GetOperator()
        {
            return " <> ";
        }
    }

    public class EqualsResolver : IResolveOperators
    {
        public bool CanProcess(ExpressionType expressionType)
        {
            return expressionType == ExpressionType.Equal;
        }

        public string GetOperator()
        {
            return " = ";
        }
    }

    public class LessThanResolver : IResolveOperators
    {
        public bool CanProcess(ExpressionType expressionType)
        {
            return expressionType == ExpressionType.LessThan;
        }

        public string GetOperator()
        {
            return " < ";
        }
    }

    public class GreaterThanResolver : IResolveOperators
    {
        public bool CanProcess(ExpressionType expressionType)
        {
            return expressionType == ExpressionType.GreaterThan;
        }

        public string GetOperator()
        {
            return " > ";
        }
    }

    public class GreaterThanOrEqualResolver : IResolveOperators
    {
        public bool CanProcess(ExpressionType expressionType)
        {
            return expressionType == ExpressionType.GreaterThanOrEqual;
        }

        public string GetOperator()
        {
            return " >= ";
        }
    }

    public class LessThanOrEqualResolver : IResolveOperators
    {
        public bool CanProcess(ExpressionType expressionType)
        {
            return expressionType == ExpressionType.LessThanOrEqual;
        }

        public string GetOperator()
        {
            return " <= ";
        }
    }

    public class AndResolver : IResolveOperators
    {
        public bool CanProcess(ExpressionType expressionType)
        {
            return expressionType == ExpressionType.AndAlso;
        }

        public string GetOperator()
        {
            return " AND ";
        }
    }

    public class OrResolver : IResolveOperators
    {
        public bool CanProcess(ExpressionType expressionType)
        {
            return expressionType == ExpressionType.OrElse;
        }

        public string GetOperator()
        {
            return " OR ";
        }
    }
    public static class ExpressionVisitorExtensions
    {
        private static string Visit(Expression expression, IOperatorResolverList lst)
        {
            dynamic dynamicExpression = expression;
            return VisitCore(dynamicExpression, lst);
        }

        private static string VisitCore(BinaryExpression bin, IOperatorResolverList lst)
        {
            if (bin.Left.NodeType == ExpressionType.Constant && ((ConstantExpression)bin.Left).Value == null
                || bin.Right.NodeType == ExpressionType.Constant && ((ConstantExpression)bin.Right).Value == null)
            {
                var openParen = "(";
                if (bin.NodeType == ExpressionType.NotEqual)
                    openParen = "(NOT ";
                if (bin.Right.NodeType == ExpressionType.Constant)
                    return openParen + Visit(bin.Left, lst) + " IS NULL) ";
                return openParen + Visit(bin.Right, lst) + " IS NULL) ";
            }

            var lvalue = Visit(bin.Left, lst);
            var op = lst.GetOperatorFor(bin.NodeType);
            var rvalue = Visit(bin.Right, lst);
            if (rvalue.Trim() == "NULL" && op.Trim() == "=")
                op = " IS ";
            return "(" + lvalue + op + rvalue + ")";
        }


        

        private static string VisitCore(MethodCallExpression method, IOperatorResolverList lst)
        {

            if (method.Method.Name == "IsNullOrEmpty")
            {
                return " IS NULL ";
            }

            if (method.Method.Name == "Trim")
            {
                var argMember = (MemberExpression)method.Arguments[0];
                var parm = argMember.GetAttribute();

                return string.Format(" IsNull({0}, '') = '' ", parm);
            }
            if (method.Method.Name == "Contains")
                return method.ParseContainsCall("");
            if (method.Method.Name == "Trim")
                return method.ParseTrimCall();
            return "";
        }

        public static string VisitCore(UnaryExpression expr, IOperatorResolverList lst)
        {
            if (expr.NodeType == ExpressionType.Convert)
            {
                var mem = (MemberExpression)expr.Operand;
                return mem.GetValue().FormatValue();
            }
            if (expr.NodeType == ExpressionType.Not)
            {
                if (expr.Operand.NodeType == ExpressionType.Call)
                {
                    var meth = (MethodCallExpression)expr.Operand;
                    return meth.ParseContainsCall(" NOT");
                }
                return "";
            }
            return "";
        }


        public static string VisitCore(MemberExpression memberExp, IOperatorResolverList lst)
        {
            if (memberExp.Expression != null
                &&
                (memberExp.Expression.NodeType == ExpressionType.Constant ||
                 memberExp.Expression.NodeType == ExpressionType.MemberAccess)
                )
            {
                return string.Format(" {0} ", memberExp.GetValue().FormatValue());
            }
            if (
                memberExp.Expression != null
                && memberExp.Expression.NodeType == ExpressionType.Parameter)
                return string.Format(" {0} ", memberExp.GetAttribute());
            if (memberExp.Expression == null)
                return string.Format(" {0} ", get_value(memberExp));
            return "";
        }



        public static string GetAttribute(this MemberExpression member)
        {
            var attrs = member.Member.GetCustomAttributes(typeof(string), false);
            if (attrs.Length == 0)
            {
                throw new InvalidExpressionException(
                    "Parameters for the SytelineInterface expression resolver must have the 'FieldName' attribute!");
            }
            return attrs[0].ToString();

        }


        private static string get_value(MemberExpression member)
        {
            if (member.Expression != null)
            {
                if (member.Expression.NodeType == ExpressionType.Constant)
                {
                    var c = (ConstantExpression)member.Expression;
                    //                    if(c.Type.Name.ToUpper().Contains("C__DISPLAYCLASS"))
                    //                    {
                    var parent = c.Value;
                    object obj = null;
                    var fieldName = member.Member.Name;
                    try
                    {
                        obj = parent.GetType().InvokeMember(fieldName, BindingFlags.GetField, null, parent,
                                                            null);

                    }
                    catch (MissingFieldException)
                    {
                        var fld = parent.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

                        obj = fld.GetValue(parent);
                    }
                    return obj.FormatValue();


                }
            }
            var prop = (PropertyInfo)member.Member;
            return string.Format("'{0}'", prop.GetValue(member.Member, null));
        }
        public static string VisitCore(ConstantExpression constant, IOperatorResolverList lst)
        {
            return string.Format(" {0} ", constant.Value.FormatValue());
        }

        public static string FormatValue(this object obj)
        {
            if (obj == null) return " NULL ";
            return obj.GetType() == typeof(string) || obj.GetType() == typeof(DateTime)
                       ? string.Format("'{0}'", obj)
                       : obj.ToString();
        }

        public static object GetValue(this MemberExpression member)
        {
            if (member.Expression.NodeType == ExpressionType.Parameter)
                return GetProperty(member);

            var parentMember = member.GetNextLevelUpObject();
            var parent = parentMember.parent;
            var fieldName = parentMember.memberName;
            return parent.GetValue(fieldName);

        }

        public static object GetValue(this   object parent, string fieldName)
        {
            object obj = null;
            try
            {
                obj = parent.GetType().InvokeMember(fieldName, BindingFlags.GetField | BindingFlags.GetProperty, null, parent,
                                                    null);

            }
            catch (MissingFieldException)
            {
                var fld = parent.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

                obj = fld.GetValue(parent);
            }
            catch (MissingMethodException)
            {
                var fld = parent.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

                obj = fld.GetValue(parent);
            }
            return obj;
        }

        public static string ParseTrimCall(this Expression exp)
        {
            var member = (MethodCallExpression)exp;
            var attr = (MemberExpression)member.Object;
            return getTrimValue(attr);

        }

        private static string getTrimValue(MemberExpression attr)
        {
            return attr.Expression.NodeType == ExpressionType.Parameter ? string.Format(" ltrim(rtrim({0})) ", GetAttribute(attr)) : string.Format(" ltrim(rtrim('{0}')) ", GetValue(attr));
        }

        public static string ParseContainsCall(this Expression exp, string negation)
        {
            var member = (MethodCallExpression)exp;
            var argMember = (MemberExpression)member.Arguments[0];
            var attr = (MemberExpression)member.Object;
            var val = argMember.GetValue();
            var parm = attr.GetAttribute();

            return string.Format(" {0}{2} LIKE '%{1}%' ", parm, val, negation);
        }

        public static Member GetNextLevelUpObject(this MemberExpression member)
        {
            var stack = addLevels(member, new Stack<Member>());
            return stack.Peek();

        }

        private static Stack<Member> addLevels(MemberExpression member, Stack<Member> stack)
        {
            if (member.Expression.NodeType == ExpressionType.Constant)
            {
                stack.Push(new Member(((ConstantExpression)member.Expression).Value, member.Member));
                return stack;
            }

            stack = addLevels((MemberExpression)member.Expression, stack);
            var p = stack.Peek();

            object obj = null;
            try
            {
                obj = p.parent.GetType().InvokeMember(p.memberName, BindingFlags.GetField | BindingFlags.GetProperty, null, p.parent,
                                                     null);
            }
            catch (MissingFieldException)
            {
                var fld = p.parent.GetType().GetField(p.memberName, BindingFlags.NonPublic | BindingFlags.Instance);

                obj = fld.GetValue(p.parent);
            }
            catch (MissingMethodException)
            {
                var fld = p.parent.GetType().GetField(p.memberName, BindingFlags.NonPublic | BindingFlags.Instance);

                obj = fld.GetValue(p.parent);
            }
            stack.Push(new Member(obj, member.Member));
            return stack;
        }

        public static string GetProperty(MemberExpression member)
        {
            var c = member;
            //                    if(c.Type.Name.ToUpper().Contains("C__DISPLAYCLASS"))
            //                    {
            var parent = c.Expression;
            object obj = null;
            var fieldName = member.Member.Name;

            obj = parent.Type.InvokeMember(fieldName, BindingFlags.GetProperty, null, parent,
                                                     null);


            return obj.ToString();
        }
    }
}