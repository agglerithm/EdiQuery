namespace EdiQuery
{
 
         
    public class GroupTypeConstants
    {
        public const string Invoice = "IN";
        public const string PurchaseOrder = "PO";
        public const string POAcknowledgement = "PR";
        public const string AdvanceShipNotice = "SH";
        public const string Inventory = "IB";

    } 
    public class EDIDateQualifiers
        {
            public const string Shipped = "011";
        }
     
    public class EDI850Constants
    {
        public const string BeginLabel = "BEG";
        public const string LineItemLabel = "PO1";
    }
    public class AddressTypeConstants
    {
        public const string ShipTo = "ST";
        public const string BillTo = "BT";
        public const string RemittanceReceiver = "RE";
        public const string SellingParty = "SE";
        public const string ShipFrom = "SF";

        public const string Vendor = "VN";
    }
}