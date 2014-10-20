using System;
using System.Collections.Generic;
using EDIDocsProcessing.Common.EDIStructures;
using EDIDocsProcessing.Common.Enumerations;

namespace EDIDocsProcessing.Common
{
    public interface ISegmentFactory
    {
        void SetBuildValues(BusinessPartner partner);
        EDIXmlSegment GetInterchangeHeader(int controlNo,   bool test);
        EDIXmlSegment GetGroupHeader(string functionalID,  int controlNumber );
        EDIXmlSegment GetDocumentHeader(string docType, int controlNumber);
        EDIXmlSegment GetDocumentFooter(int numSegments, int controlNumber);
        EDIXmlSegment GetGroupFooter(int numDocs, int controlNumber);
        EDIXmlSegment GetInterchangeFooter(int numGroups, int controlNumber);
        EDIXmlSegment GetDateTimeSegment(string dateType, DateTime dtm);
        EDIXmlSegment GetPurchaseOrderReference(string poNumber);

        EDIXmlSegment GetHierarchicalLevel(string id, string parentID,
                                           string code);

        EDIXmlSegment GetAddressName(string name, string addressType, string codeQualifier, string code);

        EDIXmlSegment GetAddressLine(string addrInfo1, string addrInfo2);

        EDIXmlSegment GetGeographicInfo(string city, string state,
                                        string zip, string country);

        EDIXmlSegment GetShipmentLineItem(string lineNum, string customerPartNum,
                                          string itemID );

        EDIXmlSegment GetShipmentLineItem(string lineNum, IEnumerable<QualifierValuePair> pairs);
        EDIXmlSegment GetLineItemShipmentDetail(string lineNum, int qtyShipped,
                                                int qtyOrdered, int qtyShippedToDate, string status);

        EDIXmlSegment GetTransactionTotal(int totalLines);

        EDIXmlSegment GetPOLine(string lineNo, 
                                int quantity, decimal price, string custPartNo,
                                string itemID, string itemDescription);
         

        EDIXmlSegment GetAckLine(string statusCode,  
                                 int quantity, string requestedShipDate, string custPartNo,
                                 string itemID, string itemDescription);
 

        EDIXmlSegment GetCurrencySegment(string qualifier, string code);
        EDIXmlSegment GetReferenceIDSegment(string qualifier, string code );
        EDIXmlSegment GetTotalMonetaryValue(decimal val);
        EDIXmlSegment GetRoutingCarrierDetails(string routingCode, 
                                               string codeQualifier, string idCode, 
                                               string transportationCode, string carrier, string statusCode);

        EDIXmlSegment GetEquipmentCarrierDetails(string code, string equipmentNumber);

        EDIXmlSegment GetQtyWeightCarrierDetails(string packaging, int qty, string qualifier, int weight, string um);
        EdiXmlBuildValues BuildValues { get; }
        EDIXmlSegment GetTerms(string typecode, decimal discountpercent, DateTime invoiceDate, int discountDays, int netDays);
        EDIXmlSegment GetLineItemInvoiceDetail(string lineNum, int quantity, decimal price, 
                                               IDictionary<Qualifier, string> detail);
        EDIXmlSegment GetProductItemDescription(string description);

        EDIXmlSegment GetServiceAllowanceAndChargeSegment(string primaryCode, string secondaryCode, decimal amount);

        EDIXmlSegment GetTaxInformationSegment(string taxType, decimal amount);
        EDIXmlSegment GetQuantitySegment(string qual, decimal qty);
        EDIXmlSegment GetCarrierDetail(string code, string carrier);
        EDIXmlSegment GetFreightOnBoard(string code);
        EDIXmlSegment GetInvoiceShipmentSummary(string uom, int unitsShipped);
        EDIXmlSegment GetMarksAndNumbersSegment(string qualifier, string numbers); 
        EDIXmlElement GetHLChildElement();
    }
}