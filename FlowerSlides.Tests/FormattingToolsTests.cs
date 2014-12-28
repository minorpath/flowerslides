using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerSlides.Tests
{
    [TestFixture]
    public class FormattingToolsTests
    {
        [Test]
        public void FormatPlantName1()
        {
            Assert.AreEqual("Penstemon fasciculatus", 
                FormattingTools.FormatPlantName("4259-penstemon-fasciculatus.jpg"));
        }
        [Test]
        public void FormatPlantName2()
        {
            Assert.AreEqual("Penstemon 'Watermelon Taffey'",
                FormattingTools.FormatPlantName("Penstemon 'Watermelon Taffey'.JPG"));
        }

        [Test]
        public void FormatPlantName3()
        {
            Assert.AreEqual("Penstemon",
                FormattingTools.FormatPlantName("Penstemon-001.jpg"));
        }

    }
}
