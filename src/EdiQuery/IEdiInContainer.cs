using System.Collections.Generic;
using EdiQuery.Structs;

namespace EdiQuery
{
    public interface IEdiInContainer
    { 
        IEnumerable<Segment> InnerSegments { get; } 
    }
}