using System.Collections.Generic;
using EdiQuery.Structs;

namespace EdiQuery
{
    public interface IAddressParser
    {
        void ProcessAddresses(List<Segment> segments, IEdiMessage ediMessage);
        int SegmentCount { get;   }
    }
}