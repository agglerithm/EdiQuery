using System.Collections.Generic;
using System.Linq;
using EdiQuery.Extensions;
using EdiQuery.Structs;

namespace EdiQuery.Containers
{
    public class DocContainer : IEdiInContainer
    {
        private readonly string _elDelimiter;

        public DocContainer(EdiSegmentCollection segs, GroupContainer parent)
        {
            _elDelimiter = segs.ElementDelimiter;
            Segments = segs.SegmentList;
            var els = Segments.First().GetElements(_elDelimiter);
            DocType = els[1];
            ControlNumber = els[2].CastToInt();
            ParentGroup = parent;
        }

        public GroupContainer ParentGroup { get; private set; }
        public string DocType { get; private set; }
        public IEnumerable<Segment> Segments { get; private set; }
        public void AddSegments(EdiSegmentCollection segs)
        { 
        }

        public IEnumerable<Segment> InnerSegments
        {
            get 
            {
                return
                    Segments; 
            }
        }

        public int ControlNumber
        {
            get; private set;
        }


        public IEdiInContainer CreateChild(EdiSegmentCollection segs)
        { 
            return null;
        }

 
    }
}