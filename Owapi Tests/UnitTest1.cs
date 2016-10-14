using System;
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
            client.GetHeroPlaytimes(battletag);
        }
    }
}
