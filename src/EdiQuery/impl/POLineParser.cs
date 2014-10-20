using System.Collections.Generic;
using System.Linq;
using EdiQuery.Extensions;
using EdiQuery.Structs;

namespace EdiQuery.impl
{
    public class POLineParser : IPOLineParser
    {
        private List<Segment> _current_line = new List<Segment>();

        #region IPOLineParser Members

        public virtual void ProcessLines(List<Segment> lst, IEdiMessage doc)
        {
            SegmentCount = 0;
            _current_line.Clear();
        }

        public int SegmentCount { get; protected set; }

        #endregion

        public CustomerOrderLine CreateLine(Segment line_seg)
        {
            if (line_seg.Label.ToString() != "PO1") return null;
            SegmentCount++;
            string el_delimiter = line_seg.Contents.Substring(3, 1);
            List<string> arr = line_seg.GetElements(el_delimiter).ToList();
            var line = new CustomerOrderLine
                           {
                               LineNumber = arr[1].CastToInt(),
                               RequestedQuantity = arr[2].CastToInt(),
                               RequestedPrice = arr[4].CastToDouble()
                           };
            for (int i = 6; i < arr.Count - 1; i++)
            {
                if (arr[i] == "IN")
                    line.CustomerPartNumber = arr[i + 1];
                if (arr[i] == "PD")
                    line.ItemDescription = arr[i + 1];
                if (arr[i] == "VN")
                    line.ItemID = arr[i + 1];
            }
            return line;
        }
    }

    public class CustomerOrderLine
    {
        public string ItemID;

        public string CustomerPartNumber
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public string ItemDescription
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public int LineNumber
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public int RequestedQuantity
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public double RequestedPrice
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}