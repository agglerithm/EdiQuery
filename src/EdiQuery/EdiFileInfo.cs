using System.Collections.Generic;
using System.Linq;
using EdiQuery.Containers;
using EdiQuery.Extensions;

namespace EdiQuery
{
    public class EdiFileInfo 
    {
        private readonly IEnumerable<InterchangeContainer> _interchanges;

        public EdiFileInfo(IEnumerable<InterchangeContainer> interchanges)
        {
            _interchanges = interchanges ?? new List<InterchangeContainer>();
            if(_interchanges.Count() == 0) return;
            SenderId = _interchanges.First().SenderId;
            var docs = new List<DocContainer>();
            _interchanges.ForEach(i => i.Groups.ForEach(g => g.Documents.ForEach(docs.Add)));
            Documents = docs;
        }
        public IEnumerable<DocContainer> Documents { get; private set; }
        public string SenderId { get; private set; }
 
    }
}