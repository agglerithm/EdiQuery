namespace EDIDocsProcessing.Common.EDIStructures
{
    using System;
    using System.Collections.Generic;

    public class QualifierValuePair
    {
        private readonly int _ndx;
        public QualifierValuePair(string q, string v, int ndx)
        {
            _ndx = ndx;
            qualifier = q;
            value = v;
        }

        private string qualifier { get; set; }
        private string value { get; set; }

        public string GetQualifierElementName(string segName)
        {
            return segName + _ndx.ToString().PadLeft(2, '0');
        }

        private string GetCodeElementName(string segName)
        {
            return segName + _ndx + 1.ToString().PadLeft(2, '0');
        }

        private bool IsValidPair()
        {
            return !(string.IsNullOrEmpty(qualifier) || string.IsNullOrEmpty(value));
        }
 
        public IEnumerable<EDIXmlElement> GetQualfierValuePair(string segmentLabel, EdiXmlBuildValues buildValues)
        {
            if (!IsValidPair()) return emptyPair(segmentLabel, buildValues);
            return new List<EDIXmlElement>
                          {
                              new EDIXmlElement(GetQualifierElementName(segmentLabel), qualifier, buildValues),
                              new EDIXmlElement(GetCodeElementName(segmentLabel), value, buildValues)
                          }; 
        }

        private IEnumerable<EDIXmlElement> emptyPair(string segmentLabel, EdiXmlBuildValues buildValues)
        {
            return new List<EDIXmlElement>
                          {
                              new EDIXmlElement(GetQualifierElementName(segmentLabel), "", buildValues),
                              new EDIXmlElement(GetCodeElementName(segmentLabel), "", buildValues)
                          };
        }
    }
}