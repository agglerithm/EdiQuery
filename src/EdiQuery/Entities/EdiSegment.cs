using System.Collections.Generic;

namespace EdiQuery.Entities
{
    public class EdiSegment
    {
        public string SegmentId { get; set; }
        public string SegmentDescription { get; set; }
        public IEnumerable<EdiElement> Elements { get; set; }
        public IEnumerable<EdiTuplet> Tuplets { get; set; }
        public int ElementCount { get; set; }
    }
}