using EdiQuery.Structs;

namespace EdiQuery.impl
{
    public class SegmentSplitter : ISegmentSplitter
    {
        public EdiSegmentCollection Split(string contents)
        {
            contents = contents.Trim();
            if (contents.Substring(0, 3) != "ISA")
            { 
                return null;
            } 
            SegmentDelimiter = contents.Substring(105, 1);
            ElementDelimiter = contents.Substring(3, 1);
            var segs = contents.GetSegmentList(SegmentDelimiter, ElementDelimiter);
            return new EdiSegmentCollection(segs, ElementDelimiter);
        }

        public string SegmentDelimiter
        {
            get; private set;
        }

        public string ElementDelimiter
        {
            get; private set;
        }
 
 
    }
}