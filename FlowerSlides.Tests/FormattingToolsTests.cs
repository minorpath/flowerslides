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

        [Test]
        public void GetLatinName_Standard1()
        {
            Assert.AreEqual("Penstemon fasciculatus",
                FormattingTools.GetLatinName(@"C:\temp\4259-PENSTEMON-FASCICULATUS.jpg"));
        }

        [Test]
        public void GetLatinName_HybridWith1Part()
        {
            Assert.AreEqual("Penstemon", FormattingTools.GetLatinName(@"C:\temp\Penstemon 'Raven'.jpg"));
        }

        [Test]
        public void GetHybridName_HybridWith1Part()
        {
            Assert.AreEqual("'Raven'", FormattingTools.GetHybridName(@"C:\temp\Penstemon 'Raven'.jpg"));
        }

        [Test]
        public void GetLatinName_HybridWith2Parts()
        {
            Assert.AreEqual("Penstemon", FormattingTools.GetLatinName(@"C:\temp\Penstemon_'Plum_Jerkin'.jpg"));
        }

        [Test]
        public void GetHybridName_HybridWith2Parts()
        {
            Assert.AreEqual("'Plum Jerkin'", FormattingTools.GetHybridName(@"C:\temp\Penstemon_'Plum_Jerkin'.jpg"));
        }

        [Test]
        public void GetLatinName_HybridWith3Parts_PenstemonChrisDeBurgh()
        {
            Assert.AreEqual("Penstemon", FormattingTools.GetLatinName(@"C:\temp\Penstemon-001 'Chris de Burgh'.jpg"));
        }
        [Test]
        public void GetHybridName_HybridWith3Parts()
        {
            Assert.AreEqual("'Chris de Burgh'", FormattingTools.GetHybridName(@"C:\temp\Penstemon-001 'Chris de Burgh'.jpg"));
        }

        [Test]
        public void GetLatinName_HybridWith3Parts_ClematisVilleDeLyon()
        {
            Assert.AreEqual("Clematis", FormattingTools.GetLatinName(@"C:\temp\Clematis 'Ville de Lyon'.jpg"));
        }

        [Test]
        public void GetHybridName_HybridWith3Parts_ClematisVilleDeLyon()
        {
            Assert.AreEqual("'Ville de Lyon'", FormattingTools.GetHybridName(@"C:\temp\Clematis 'Ville de Lyon'.jpg"));
        }
    }
}
