using EdiQuery.Extensions;

namespace EdiQuery.Entities
{
    public class EdiTuplet
    {
        public EdiTuplet(string[] elements, int ndx, string segLabel)
        {
           Qualifier = new EdiElement(segLabel.ElementLabel(ndx),elements[ndx]);
            Info = new EdiElement(segLabel.ElementLabel(ndx + 1), elements[ndx + 1]);
        }

        public EdiElement Qualifier { get; set; }
        public EdiElement Info { get; set; }
    }
}