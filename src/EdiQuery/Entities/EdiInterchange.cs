using System;
using System.Collections.Generic;
using System.Linq;
using EdiQuery.Containers;
using EdiQuery.Extensions;
using EdiQuery.Structs;

namespace EdiQuery.Entities
{
    public class EdiInterchange:EdiEnvelope 
    {

        public EdiInterchange(EdiSegmentCollection segs):base(segs)
        {  
             
        }
        public EdiTuplet SenderId { get; set; }
        public EdiTuplet ReceiverId { get; set; }
        public DateTime InterchangeDate { get; set; }
        public bool Test { get; set; } 
        public EdiTuplet SecurityData { get; set; }
        public EdiTuplet AuthorizationData { get; set; }
        public bool AcknowledgementRequested { get; set; }
        public string SubelementSeparator { get; set; }
        public string StandardsId { get; set; }
        public string Version { get; set; }
        public IEnumerable<EdiFunctionalGroup> Groups { get; set; }
        public int GroupCount { get; set; }
        protected override void _parseFooter()
        {
            GroupCount = int.Parse(_footerArray[1]);
        }

        protected override void _parseHeader()
        {
            SenderId = new EdiTuplet(_headerArray, 5, "ISA");
            ReceiverId = new EdiTuplet(_headerArray, 7, "ISA"); 
            SecurityData =  new EdiTuplet(_headerArray,3,"ISA");
            AuthorizationData = new EdiTuplet(_headerArray, 3, "ISA");
            Test = _headerArray[15] == "T";
            AcknowledgementRequested = _headerArray[14] != " ";
            Version = _headerArray[12];
            InterchangeDate = _headerArray[9].DateTimeFromEdiDateTime(_headerArray[10]);
        }

        protected override SegmentLabel headerLabel()
        {
            return SegmentLabel.InterchangeLabel;
        }

        protected override SegmentLabel footerLabel()
        {
            return SegmentLabel.InterchangeClose;
        }
    }
}