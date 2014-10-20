using System.Collections.Generic;
using EdiQuery.Structs;

namespace EdiQuery
{
    public interface IEdiInContainer
    {
        void AddSegments(EdiSegmentCollection segs);
        IEnumerable<Segment> InnerSegments { get; }
        IEdiInContainer CreateChild(EdiSegmentCollection segs);
    }
}