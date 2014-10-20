using System;
using System.Collections.Generic;
using System.Linq;
using EdiQuery.Structs;

namespace EdiQuery.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this IList<T> lst, IEnumerable<T> source)
        {
            source.ForEach(lst.Add);
        }
        public static void ForEach<T>(this IEnumerable<T> lst, Action<T> actn)
        {
            if(lst == null)
                throw new ApplicationException("Cannot call 'ForEach' on a non-existant list!");
             lst.ToList().ForEach(actn);
        }

        public static T Find<T>(this IEnumerable<T> lst, Predicate<T> actn)
        {
            return lst.ToList().Find(actn);
        }

        public static List<List<Segment>> SplitLoop(this IEnumerable<Segment> lst)
        {
            var listList = new List<List<Segment>>();
            var looplabel = lst.First().Label;
            var subList = new List<Segment>();
            lst.ForEach(seg => build_list_list(seg, looplabel, ref subList, listList));
            listList.Add(subList);
            return listList;
        }
        private static void build_list_list(Segment seg, SegmentLabel looplabel, ref List<Segment> subList, List<List<Segment>> listList)
        {
            if (seg.Label == looplabel && subList.Count > 0)
            {
                listList.Add(subList);
                subList = new List<Segment>();
            }
            subList.Add(seg);
        }
        public static void RemoveWhile(this  List<Segment> segs, Func<Segment, bool> predicate)
        {
            var lst = segs.TakeWhile(predicate);
            lst.ForEach(seg => segs.Remove(seg));
        }
        public static Segment FindSegmentByLabel(this IEnumerable<Segment> segs,
                                        SegmentLabel labelValue)
        {
            return segs.Find(seg => seg.Label == labelValue);
        }

    }
}
