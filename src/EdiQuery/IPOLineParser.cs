using System.Collections.Generic;
using EdiQuery.Structs;

namespace EdiQuery
{
    public interface IPOLineParser
    {
        int SegmentCount { get; }
        void ProcessLines(List<Segment> lst, IEdiMessage doc);
    }
}