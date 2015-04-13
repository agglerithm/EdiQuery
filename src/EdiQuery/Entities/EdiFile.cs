using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EdiQuery.Containers;
using EdiQuery.impl;

namespace EdiQuery.Entities
{
    public class EdiFile
    {
        private EdiFileContainer _container;
        public string Contents { get; private set; }
        public string FileName { get; private set; }
        public string FullPath { get; private set; }

        public EdiFile(string path)
        {
            FullPath = path;
            FileName = Path.GetFileName(path);
            if (!File.Exists(path)) return;
            var strm = File.OpenText(path);
            Contents = strm.ReadToEnd();
            strm.Close();
        }

        public IEnumerable<EdiInterchange> Interchanges
        {
            get { return _container.Interchanges; }
        }

        public EdiFileContainer Container{get
        {
            if (_container == null)
            {
                var segSplitter = new SegmentSplitter(); 
                _container = new EdiFileContainer(segSplitter.Split(Contents));
            }
            return _container;
        }}
    }
}
