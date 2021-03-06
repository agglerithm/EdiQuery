﻿using System.Reflection;

namespace EdiQuery.Extensions
{
    public class Member
    {
        public Member(object obj, MemberInfo inf)
        {
            parent = obj;
            info = inf;
        }
        public object parent { get; private set; }
        public string memberName { get { return info.Name; } }
        public MemberInfo info { get; private set; }
    }
}