using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace OwapiClient
{
    public class OClient
    {
        private RestRequest GetBlobRequest(string battletag)
        {
            return CreateBaseRequest(battletag, "blob");
        }

        private RestRequest GetUserStats(string battletag)
        {
            return CreateBaseRequest(battletag, "stats");
        }

        private RestRequest GetAchievements(string battletag)
        {
            return CreateBaseRequest(battletag, "achievements");
        }

        private RestRequest GetHeroStats(string battletag)
        {
            return CreateBaseRequest(battletag, "heroes");
        }

        private RestRequest CreateBaseRequest(string battletag, string method)
        {
            RestRequest request = new RestRequest("api/v3/u/{battletag}/{method}", Method.GET);
            request.AddUrlSegment("battletag", FormatBattletag(battletag));
            request.AddUrlSegment("method", method);
            return request;
        }

        private string FormatBattletag(string battletag)
        {
            return battletag.Replace('#', '-').Replace(" ", "");
        }

        public Playtime GetHeroPlaytimes(string battletag)
        {
            RestClient client = new RestClient("https://owapi.net");
            string responseJson = client.Execute(GetBlobRequest(battletag)).Content;
            OwapiResponse re = JsonConvert.DeserializeObject<OwapiResponse>(responseJson);
            return re.UsRegion.Heroes.Playtime;
        }

        public int GetCompetitiveRank(string battletag)
        {
            string responseJson = new RestClient("https://owapi.net").Execute(GetUserStats(battletag)).Content;
            JObject jObject = JObject.Parse(responseJson);
            return (int) jObject.SelectToken("us.stats.competitive.overall_stats.comprank");
        }
    }
}