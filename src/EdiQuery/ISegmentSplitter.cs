using EdiQuery.Structs;

namespace EdiQuery
{
    public interface ISegmentSplitter
    {
        EdiSegmentCollection Split(string contents);
    }
}