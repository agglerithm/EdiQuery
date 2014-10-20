namespace EdiQuery.Structs
{ 
    public class SegmentLabel 
    {
        public static SegmentLabel ProductItemDescription = new SegmentLabel("PID", 0);
        public static SegmentLabel PricingInformation = new SegmentLabel("CTP", 1);
        public static SegmentLabel DateTimeReference = new SegmentLabel("DTM", 2);
        public static SegmentLabel SalesRequirements = new SegmentLabel("CSH", 3);
        public static SegmentLabel CarrierDetails = new SegmentLabel("TD5", 4);
        public static SegmentLabel ScheduleLabel = new SegmentLabel("SCH", 5);
        public static SegmentLabel ContactLabel = new SegmentLabel("PER", 6);
        public static SegmentLabel SummaryLabel = new SegmentLabel("CTT", 7);
        public static SegmentLabel ReferenceLabel = new SegmentLabel("REF", 8);
        public static SegmentLabel InterchangeLabel = new SegmentLabel("ISA", 9);
        public static SegmentLabel GroupLabel = new SegmentLabel("GS", 10);
        public static SegmentLabel DocumentLabel = new SegmentLabel("ST", 11);
        public static SegmentLabel AddressNameLabel = new SegmentLabel("N1", 12);
        public static SegmentLabel AddressLineLabel = new SegmentLabel("N3", 13);
        public static SegmentLabel GeographicLabel = new SegmentLabel("N4", 14);
        public static SegmentLabel DocumentClose = new SegmentLabel("SE", 15);
        public static SegmentLabel GroupClose = new SegmentLabel("GE", 16);
        public static SegmentLabel InterchangeClose = new SegmentLabel("IEA", 17);
        public static SegmentLabel ReferenceId = new SegmentLabel("N9", 18);
        public static SegmentLabel PurchaseOrder = new SegmentLabel("PO1", 19);
        public static SegmentLabel POBegin = new SegmentLabel("BEG", 20);
        public static SegmentLabel PurchaseOrderChange = new SegmentLabel("POC", 21);
        public static SegmentLabel POChangeBegin = new SegmentLabel("BCH", 22);
        public static SegmentLabel CurrencyLabel = new SegmentLabel("CUR", 23);
        public static SegmentLabel TaxRefLabel = new SegmentLabel("TAX", 24);
        public static SegmentLabel FreightOnBoardLabel = new SegmentLabel("FOB", 25);
        public static SegmentLabel Message = new SegmentLabel("MSG", 26);
        public static SegmentLabel Package = new SegmentLabel("PKG", 27);
        public static SegmentLabel Ack = new SegmentLabel("AK1", 28);
        public static SegmentLabel AckDetail = new SegmentLabel("AK9", 29);
        public static SegmentLabel Terms = new SegmentLabel("ITD", 30);
        public static SegmentLabel Amount = new SegmentLabel("AMT", 31);
        public static SegmentLabel BFRBegin = new SegmentLabel("BFR", 32);
        public static SegmentLabel UIT = new SegmentLabel("UIT", 33);
        public static SegmentLabel QTY = new SegmentLabel("QTY", 34);
        public static SegmentLabel FST = new SegmentLabel("FST", 35);
        public static SegmentLabel LIN = new SegmentLabel("LIN", 36);
        public static SegmentLabel InvoiceBegin = new SegmentLabel("BIG", 37);
        public static SegmentLabel IT1 = new SegmentLabel("IT1", 38);
        public static SegmentLabel TDS = new SegmentLabel("TDS", 39);
        public static SegmentLabel TXI = new SegmentLabel("TXI", 40);
        public static SegmentLabel CAD = new SegmentLabel("CAD", 41);
        public static SegmentLabel TransactionAck = new SegmentLabel("AK2", 42);
        public static SegmentLabel AckDateSegmentNote = new SegmentLabel("AK3", 43);
        public static SegmentLabel AckDataElement = new SegmentLabel("AK4", 44);
        public static SegmentLabel AckFooter = new SegmentLabel("AK5", 45);


        private SegmentLabel(string displayName, int value)
        {
            Name = displayName; 
        }

        private string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public SegmentLabel()
        {
        }

        public static T FromText<T>(string str)
        {
            throw new System.NotImplementedException();
        }
    }
}