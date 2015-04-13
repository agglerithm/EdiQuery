using System.Collections.Generic;
using EdiQuery.Structs;

namespace EdiQuery.Entities
{
    public class EdiTransactionSet:EdiEnvelope
    {
        public EdiTransactionSet(EdiSegmentCollection segs) : base(segs)
        {
        }

        public string IdCode { get; set; } 
        public int SegmentCount { get; set; }
        protected override void _parseFooter()
        {
            SegmentCount = int.Parse(_footerArray[1]);
        }

        protected override void _parseHeader()
        {
            IdCode = _headerArray[1]; 
        }

        protected override SegmentLabel headerLabel()
        {
            return SegmentLabel.DocumentLabel;
        }

        protected override SegmentLabel footerLabel()
        {
            return SegmentLabel.DocumentClose;
        }
    }
}