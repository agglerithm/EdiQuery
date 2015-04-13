using System.Collections.Generic;
using System.Linq;
using EdiQuery.Structs;

namespace EdiQuery.Entities
{
    public abstract class EdiEnvelope
    {
        protected readonly EdiSegmentCollection _segments;
        protected string[] _footerArray;
        protected string[] _headerArray;

        public EdiEnvelope(EdiSegmentCollection segs)
        {
            Header = segs.FirstWith(headerLabel());
            Footer = segs.FirstWith(footerLabel());
            _footerArray = Footer.GetElements(segs.ElementDelimiter);
            _headerArray = Header.GetElements(segs.ElementDelimiter);
            if (Header == null || Footer == null)
                throw new EdiBadlyFormedEnvelopeException("Header/Footer not found in envelope!");
            ControlNumber = Footer.GetElements(segs.ElementDelimiter)[1];
            _segments = segs;
            _parseHeader();
            _parseFooter();
        }

        protected abstract void _parseFooter();

        protected abstract void _parseHeader();

        public string ControlNumber { get; set; }
        public Segment Header { get; private set; }
        public Segment Footer { get; private set; }
        protected abstract SegmentLabel headerLabel();
        protected abstract SegmentLabel footerLabel();
        public IEnumerable<Segment> InnerSegments
        {
            get
            {
                return _segments.SegmentList.Where(s => s.Label != Header.Label &&
                                                        s.Label != Footer.Label);
            }
        }

    }
}