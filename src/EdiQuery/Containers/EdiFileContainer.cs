using System.Collections.Generic;
using System.Linq;
using EdiQuery.Entities;
using EdiQuery.Structs;

namespace EdiQuery.Containers
{
    public class EdiFileContainer : IEdiInContainer
    {
        private readonly IList<EdiInterchange> _interchanges = new List<EdiInterchange>();
        private EdiSegmentCollection _segments;

        public IQueryable<T> Query<T>()
        {
            return null;
        }
        public EdiFileContainer(EdiSegmentCollection segs)
        {
            _segments = segs;
        }
 

        public IEnumerable<Segment> InnerSegments
        {
            get { return _segments.SegmentList; }
        }

        public IEnumerable<EdiInterchange> Interchanges
        {
            get { return _interchanges; } 
        }

 
 
    }
}