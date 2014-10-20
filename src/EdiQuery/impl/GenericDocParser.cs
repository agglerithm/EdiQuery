using System;
using System.Collections.Generic;
using EdiQuery.Structs;

namespace EdiQuery.impl
{
    public class GenericDocParser : IDocumentParser
    {
        protected IAddressParser _addrParser;
        protected string _doc_id;

        public GenericDocParser(IAddressParser addressParser, string doc_id)
        {
            _doc_id = doc_id;
            _addrParser = addressParser;
        }


        public bool CanProcess(Segment header)
        {
            string el = get_element_delimiter(header);
            string[] arr = header.Contents.Split(el.ToCharArray());
            return arr[1] == _doc_id;
        }


        public string ElementDelimiter { get; private set; }

        public string SegmentDelimiter { get; set; }

        public T Process<T>(List<Segment> seg_list) where T : IEdiMessage
        {
            ElementDelimiter = get_element_delimiter(seg_list[0]);
            return (T) process_segment_list(seg_list);
        }


        private static string get_element_delimiter(Segment header)
        {
            return header.Contents.Substring(2, 1);
        }

        protected virtual IEdiMessage process_segment_list(List<Segment> doc_list)
        {
            throw new NotImplementedException();
        }
    }
}