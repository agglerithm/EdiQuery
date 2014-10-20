using System.Collections.Generic;
using EDIDocsProcessing.Core.DocsIn.impl;

namespace EDIDocsProcessing.Core.DocsIn
{
    public interface ISplitter
    {
        List<IEnumerable<Segment>> SplitByGroup(List<Segment> segments);
        List<IEnumerable<Segment>> SplitByInterchange(List<Segment> segments);
        List<IEnumerable<Segment>> SplitByDocument(List<Segment> segments);
    }
}