using System.Collections.Generic;
using System.Linq;
using EdiQuery.Structs;

namespace EdiQuery.Containers
{
    public class EdiFileContainer : IEdiInContainer
    {
        private readonly IList<InterchangeContainer> _interchanges = new List<InterchangeContainer>();
        private EdiSegmentCollection _segments;

        public IQueryable<T> Query<T>()
        {
            return new EdiQuery();
        }
        public EdiFileContainer(EdiSegmentCollection segs)
        {
            _segments = segs;
        }
        public void AddSegments(EdiSegmentCollection segs)
        {
            CreateChild(segs);
        }

        public IEnumerable<Segment> InnerSegments
        {
            get { return _segments.SegmentList; }
        }

        public IEnumerable<InterchangeContainer> Interchanges
        {
            get { return _interchanges; } 
        }


        public IEdiInContainer CreateChild(EdiSegmentCollection segs)
        {
            var child = new InterchangeContainer(segs);
            _interchanges.Add(child);
            return child;
        }
 
    }
}