using AFPST.Common.Structures;
using EDIDocsProcessing.Common.Enumerations;
using EDIDocsProcessing.Core.DocsOut.EDIStructures;

namespace EDIDocsProcessing.Common.EDIStructures
{
    using EdiMessages;

    public class AddressLoop: EDIXmlMixedContainer 
    {
        private readonly ISegmentFactory _factory;
        public AddressLoop(ISegmentFactory factory) : base(EdiStructureNameConstants.Loop)
        {
            _factory = factory;
        }

        public void AddAddress(Address addr, Qualifier codeQualifier)
        { 
            AddSegment(_factory.GetAddressName(addr.AddressName, addr.AddressType, codeQualifier.Value, addr.AddressCode.CustomerCode));
            AddSegment(_factory.GetAddressLine(addr.Address1, addr.Address2));
            AddSegment(_factory.GetGeographicInfo(addr.City, addr.State, addr.Zip, addr.Country));
        }
    }
}
