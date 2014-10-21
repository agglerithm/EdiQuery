using System.Collections.Generic;
using System.Linq.Expressions;

namespace EdiQuery.Extensions
{

    public interface IResolveOperators
    {
        bool CanProcess(ExpressionType expressionType);
        string GetOperator();
    }
    public interface IOperatorResolverList
    {
        string GetOperatorFor(ExpressionType expType);
    }
    public class OperatorResolverList : IOperatorResolverList
    { 
        private IList<IResolveOperators> _opResolvers = new List<IResolveOperators>();
        public OperatorResolverList()
        { 
            _opResolvers.Add(new GreaterThanResolver());
            _opResolvers.Add(new LessThanResolver());
            _opResolvers.Add(new EqualsResolver());
            _opResolvers.Add(new InequalityResolver());
            _opResolvers.Add(new AndResolver());
            _opResolvers.Add(new OrResolver());
            _opResolvers.Add(new GreaterThanOrEqualResolver());
            _opResolvers.Add(new LessThanOrEqualResolver());
        }

        public string GetOperatorFor(ExpressionType expType)
        {
            return _opResolvers.Find(r => r.CanProcess(expType)).GetOperator();
        }
    }
}