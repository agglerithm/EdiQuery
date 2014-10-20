using System.Collections.Generic;
using EdiQuery.Structs;

namespace EdiQuery
{
    public interface IDocumentParser
    {
        string ElementDelimiter { get; }
        string SegmentDelimiter { get; set; }
        bool CanProcess(Segment header);
        T Process<T>(List<Segment> seg_list) where T : IEdiMessage;
    }
}