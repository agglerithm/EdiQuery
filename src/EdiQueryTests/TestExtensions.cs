using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EdiQuery.Extensions;
using NUnit.Framework;

namespace EdiQueryTests
{
    [TestFixture]
    public class TestExtensions
    {
        [Test]
        public void can_format_element_label()
        {
            var isaLabel = "ISA".ElementLabel(5);
            Assert.That(isaLabel == "ISA05");
            isaLabel = "ISA".ElementLabel(11);
            Assert.That(isaLabel == "ISA11");
        }

        [Test]
        public void can_parse_edi_date_time()
        {
            var dte = new DateTime(2005, 5, 20, 10, 20, 50);
            var ediDte = "20050520".DateTimeFromEdiDateTime("102050");
            Assert.That(dte == ediDte);
        }
    }
}
