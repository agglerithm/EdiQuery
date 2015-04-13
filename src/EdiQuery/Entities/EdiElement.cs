using System.Collections.Generic;
using EdiQuery.Structs;

namespace EdiQuery.Entities
{
    public class EdiElement
    {
        public EdiElement(string lbl, string val)
        {
            Label = lbl;
            Value = val;
        }

        public string Label { get; private set; }
        public string Value { get; private set; }
    }
}