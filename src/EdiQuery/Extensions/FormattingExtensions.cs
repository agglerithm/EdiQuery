using System.Collections.Generic;
using System.Linq;
using EdiQuery.Extensions;
using EdiQuery.Structs;

namespace EdiQuery
{
    public static class FormattingExtensions
    {
//        public static string FormatByTransport(this int num, TransportAgent agent)
//        {
//            if (agent == TransportAgent.Edict)
//                return num.ToString("00000000#");
//            return num.ToString();
//        }

        public static IList<Segment> GetSegmentList(this string str, string segDelim, string elDelim)
        {
            var segList = str.Split(segDelim.ToCharArray());
            var lst = new List<Segment>();
            segList.Where(s => s != null && s.Trim() != "")
                .ForEach(s => lst.Add(GetSegment(s, elDelim)));
            return lst;
        }
        public static Segment GetSegment(this string str, string delim)
        {
            var seg = new Segment { Contents = str };
            string[] els = str.Split(delim.ToCharArray());
            seg.Label = els[0].GetSegmentLabel();
            return seg;
        }

        public static SegmentLabel GetSegmentLabel(this string str)
        {

            return SegmentLabel.FromText<SegmentLabel>(str);
        }

//        public static ChangeType GetChangeType(this string str)
//        {
//            return EnumerationOfInteger.FromText<ChangeType>(str);
//        }
    }
}
