using System;
using System.Collections.Generic;
using EdiQuery.Extensions;
using EdiQuery.Structs;

namespace EdiQuery
{
    public static class EDIUtilities
    { 
        public static bool MoveSegmentByLabel(List<Segment> source, List<Segment> dest, SegmentLabel lbl)
        {
            Segment temp_seg = source.FindSegmentByLabel(lbl);
            if (temp_seg == null) return false;
            dest.Add(temp_seg);
            source.Remove(temp_seg);
            return true;
        }

        public static void MoveSegment(List<Segment> source, List<Segment> dest, Segment seg)
        {
            source.Remove(seg);
            dest.Add(seg);
        }

        public static void ProcessFooter(Segment seg, IEdiMessage ediMessage, string ElementDelimiter,
                                 int segmentsProcessed)
        {
            int expected = seg.GetElements(ElementDelimiter)[1].CastToInt();
            if (segmentsProcessed != expected)
                throw new InvalidEDIDocumentException("Segments processed does not match included segment count! Segments processed = " + segmentsProcessed + "; expected " + expected);
            if (ediMessage.ControlNumber != seg.GetElements(ElementDelimiter)[2])
                throw new InvalidEDIDocumentException("Control numbers in _header and _footer do not match!");
        }

        public static void ProcessFooter(List<Segment> seg_list, IEdiMessage ediMessage, string ElementDelimiter,
                                         int segmentsProcessed)
        {
            Segment temp_seg = seg_list.FindSegmentByLabel(SegmentLabel.DocumentClose);
            int expected = temp_seg.GetElements(ElementDelimiter)[1].CastToInt();
            if (segmentsProcessed != expected)
                throw new InvalidEDIDocumentException("Segments processed does not match included segment count! Segments processed = " + segmentsProcessed + "; expected " + expected);
            if (ediMessage.ControlNumber != temp_seg.GetElements(ElementDelimiter)[2])
                throw new InvalidEDIDocumentException("Control numbers in _header and _footer do not match!");
            seg_list.Remove(temp_seg);
        }
    }
    public class InvalidEDIDocumentException : Exception
    {
        public InvalidEDIDocumentException(string message)
            : base(message)
        {
        }
    }
    public interface IEdiMessage
    {
        string ControlNumber { get; set; }
    }
}