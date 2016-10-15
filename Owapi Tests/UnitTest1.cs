using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OwapiClient;

namespace Owapi_Tests
{
    [TestClass]
    public class OwapiTests
    {
        public const string battletag = "MaybeMonad#11686";
        [TestMethod]
        public void GetBlob()
        {
            OClient client = new OClient();
            Debug.WriteLine(client.GetCompetitiveRank(battletag));
        }
    }
}
