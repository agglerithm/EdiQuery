using System.Collections.Generic;
using EdiQuery.Extensions;
using EdiQuery.Structs;

namespace EdiQuery.impl
{
    public class AddressParser : IAddressParser
    {
 
        #region IAddressParser Members

/*
        public int ProcessAddresses(List<Segment> lst, IEdiMessage ediMessage)
        {
            if (!addressesFound(lst)) return 0;
            lst.RemoveWhile(seg => seg.Label != SegmentLabel.AddressNameLabel);
            var addrLoop = new List<Segment>();
            var segmentCount = 0;
            while (EDIUtilities.MoveSegmentByLabel(lst, addrLoop, SegmentLabel.AddressNameLabel))
            {
                segmentCount++;
                Segment nextSeg = lst[0];
                while (nextSeg.Label == SegmentLabel.AddressLineLabel
                       || nextSeg.Label == SegmentLabel.GeographicLabel
                       || nextSeg.Label == SegmentLabel.ContactLabel)
                {
                    segmentCount++;
                    EDIUtilities.MoveSegment(lst, addrLoop, nextSeg);
                    nextSeg = lst[0];
                }
                ediMessage.AddAddress(process_address(addrLoop));
                addrLoop.Clear();
            }
            return segmentCount;
        }
*/

        public int SegmentCount { get; private set; }

        private bool addressesFound(List<Segment> lst)
        {
            return lst.Find(seg => seg.Label == SegmentLabel.AddressNameLabel) != null;
        }

/*
        public int ProcessAddresses(List<Segment> segList, ChangeOrderMessage ediMessage)
        {
            var addressList = segList.SplitLoop();
            addressList.ForEach(a => ediMessage.AddAddress(process_address(a)));
            return 0;
        }
*/

        #endregion

//        private static Address process_address(List<Segment> addressSegments)
//        {
//            var addrStruct = new Address();
//            string elementDelimeter = addressSegments[0].Contents.Substring(2, 1);
//            addressSegments.ForEach(line => process_address_line(line, addrStruct, elementDelimeter));
//            return addrStruct;
//        }

/*
        private static void process_address_line(Segment line, Address address, string elementDelimiter)
        {
            var arr = line.GetElements(elementDelimiter);
            if (line.Label == SegmentLabel.AddressNameLabel)
            {
                address.AddressType = get_address_type(arr[1]);
                address.AddressName = arr[2];
                address.AddressCode = new AddressCode();
                if (arr.Length >= 5)
                {
                    address.AddressCode.CustomerCode = arr[4] ;
                }
            }
            if (line.Label == SegmentLabel.AddressLineLabel)
                if (string.IsNullOrEmpty(address.Address1))
                    address.Address1 = arr[1];
                else
                {
                    if (string.IsNullOrEmpty(address.Address2))
                        address.Address2 = arr[1];
                }
            if (line.Label == SegmentLabel.GeographicLabel)
            {
                address.City = arr[1];
                address.State = arr[2];
                address.Zip = arr[3];
                if(arr.Length > 4)
                    address.Country = arr[4];
            } 
            if (line.Label == SegmentLabel.ContactLabel)
            {
                address.ContactName = arr[2];
                if (arr.Length > 3)
                    address.PhoneNumber = arr[4];
            }
        }
*/


        private static string get_address_type(string code)
        {
            return code == "ST" ? AddressTypeConstants.ShipTo : AddressTypeConstants.BillTo;
        }

        void IAddressParser.ProcessAddresses(List<Segment> segments, IEdiMessage ediMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}