using EdiQuery.Structs;

namespace EdiQuery

{
    public interface IEdiFileReader
    {
        EdiFileInfo Read(EdiSegmentCollection segments);
    }
public class EdiFileReader : IEdiFileReader
    {
        private readonly IHierarchySplitter _hierarchySplitter;

        public EdiFileReader(IHierarchySplitter hierarchySplitter)
        {
            _hierarchySplitter = hierarchySplitter;
        }

        public EdiFileInfo Read(EdiSegmentCollection segments)
        {
            var interchanges = _hierarchySplitter.SplitByInterchange(segments);
            return new EdiFileInfo(interchanges);
        }
 
 
    }
}