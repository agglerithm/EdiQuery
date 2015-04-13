using System;
using System.Collections.Generic;
using EdiQuery.Extensions;
using EdiQuery.Structs;

namespace EdiQuery.Entities
{
    public class EdiFunctionalGroup:EdiEnvelope
    {
        public EdiFunctionalGroup(EdiSegmentCollection segs) : base(segs)
        {
        }

        public string FunctionalId { get { return _headerArray[1]; } }
        public string SenderCode { get { return _headerArray[2]; } }
        public string ReceiverCode { get { return _headerArray[3]; } }
        public DateTime GroupDate { get { var datePart = _headerArray[4];
        var timePart =  _headerArray[5]; 
            return new DateTime();
        } }

        public string Version { get { return _headerArray[8]; } }
        public string Agency { get { return _headerArray[7]; } }
        public string IdCode { get { return _headerArray[6]; } }
        public IEnumerable<EdiTransactionSet> TransactionSets { get; set; }
        public int TransactionSetCount { get; set; }
        protected override void _parseFooter()
        {
            TransactionSetCount = _footerArray[1].CastToInt();
        }

 

        protected override SegmentLabel headerLabel()
        {
            return Header.Label;
        }

        protected override SegmentLabel footerLabel()
        {
            return Footer.Label;
        }
    }
}