using System.Collections.Generic;
using EdiQuery.Containers;
using EdiQuery.Structs;

namespace EdiQuery
{
    public interface IHierarchySplitter
    {
        IEnumerable<GroupContainer> SplitByGroup(EdiSegmentCollection segments, InterchangeContainer parent);
        IEnumerable<InterchangeContainer> SplitByInterchange(EdiSegmentCollection segments);
        IEnumerable<DocContainer> SplitByDocument(EdiSegmentCollection segments, GroupContainer parent);
    }
}