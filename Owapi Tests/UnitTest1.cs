using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OwapiClient;

namespace Owapi_Tests
{
    [TestClass]
    public class OwapiTests
    {
        public const string battletag = "MaybeMonad-11686";
        [TestMethod]
        public void GetBlob()
        {
            BlizzScraper b = new BlizzScraper(battletag,"us");
            int y =b.GetRank();
            Assert.AreEqual(2151,y);

            //Dictionary<string, TimeSpan> heroTimesQuickplay = b.GetHeroTimesQuickplay();
            //foreach (KeyValuePair<string, TimeSpan> keyValuePair in heroTimesQuickplay)
            //{
            //    Console.WriteLine($"{keyValuePair.Key} - {keyValuePair.Value.TotalHours}");
            //}
            Dictionary<string, TimeSpan> heroTimesCompetitive = b.GetHeroTimesCompetitive();
            //foreach (KeyValuePair<string, TimeSpan> keyValuePair in heroTimesCompetitive)
            //{
            //    Console.WriteLine($"{keyValuePair.Key} - {keyValuePair.Value.TotalHours}");
            //}
        }
    }
}
