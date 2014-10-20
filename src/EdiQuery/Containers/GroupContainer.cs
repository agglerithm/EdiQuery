using System;
using System.Collections.Generic;
using System.Linq;
using EdiQuery;
using EdiQuery.Containers;
using EdiQuery.Extensions;
using EdiQuery.Structs;

public class GroupContainer : IEdiInContainer
{
    private EdiSegmentCollection _segments;
    private IList<DocContainer> _documents = new List<DocContainer>();

    public GroupContainer(EdiSegmentCollection segs)
    {
        _segments = segs;
        var groupHeader = _segments.FirstWith(SegmentLabel.GroupLabel);
        if (groupHeader == null)
            return;
        var arr = groupHeader.GetElements(segs.ElementDelimiter);
        GroupId = arr[1].Trim();
        DateSent = arr[4].DateFromEDIDate();
        ControlNumber = arr[5].Trim();
    }

    public string ControlNumber
    {
        get;
        private set;
    }
    public DateTime DateSent
    { get; private set; }

    public string GroupId
    {
        get;
        private set;
    }
    public IEnumerable<DocContainer> Documents
    {
        get { return _documents; }
        private set { _documents = (IList<DocContainer>)value; }
    }

    public void AddSegments(EdiSegmentCollection segs)
    {
        _segments = segs;
    }

    public IEnumerable<Segment> InnerSegments
    {
        get
        {
            return
                _segments.SegmentList.Where(
                    s =>
                    s.Label != SegmentLabel.GroupLabel && s.Label != SegmentLabel.GroupClose);
        }
    }

    public IEdiInContainer CreateChild(EdiSegmentCollection segs)
    {
        var child = new DocContainer(segs, this);
        _documents.Add(child);
        return child;
    }

    public void AddDocuments(IEnumerable<DocContainer> docs)
    {
        _documents.Clear();
        _documents.AddRange(docs);
    }


}