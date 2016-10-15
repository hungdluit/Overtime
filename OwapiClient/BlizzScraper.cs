using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace OwapiClient
{
    public class BlizzScraper
    {
        public string Battletag { get; set; }
        public string Region { get; set; }
        public HtmlDocument CachedPage { get; set; }
        public string BaseUrl
        {
            get { return "https://playoverwatch.com/en-us/career/pc/" + Region + "/" + Battletag; }
        }


        public BlizzScraper(string battletag, string region)
        {
            Battletag = battletag;
            Region = region;
            //CachedPage = GetPageDoc(BaseUrl);
        }

        public HtmlDocument GetPageDoc(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse resp = request.GetResponse();
            HtmlDocument doc = new HtmlDocument();
            doc.Load(resp.GetResponseStream());
            return doc;
        }

        public void RefreshPage() => CachedPage = GetPageDoc(BaseUrl);

        public int GetRank()
        {
            RefreshPage();
            return int.Parse(CachedPage.DocumentNode.SelectSingleNode(".//div[@class='competitive-rank']/div").InnerText);
        }

        public Dictionary<string, TimeSpan> GetHeroTimesQuickplay()
        {
            RefreshPage();
            Dictionary<string, TimeSpan> heroes = new Dictionary<string, TimeSpan>();
            HtmlNode comparisons = CachedPage.DocumentNode.SelectSingleNode(".//div[@data-group-id='comparisons']");
            HtmlNodeCollection nodes = comparisons.SelectNodes("//div[@class='bar-text']");
            foreach (HtmlNode heroNode in nodes)
            {

                string name = heroNode.FirstChild.InnerText;
                if (!heroes.ContainsKey(name))
                {
                    string time = heroNode.ChildNodes[1].InnerText.ToLower();
                    if (time.Contains("minute"))
                    {
                        TimeSpan timeS = TimeSpan.FromMinutes(int.Parse(time.Split(' ')[0]));

                        heroes[name] = timeS;
                    }
                    else if (time.Contains("hour"))
                    {
                        TimeSpan timeS = TimeSpan.FromHours(int.Parse(time.Split(' ')[0]));
                        heroes[name] = timeS;
                    }
                    else
                    {
                        heroes[name] = TimeSpan.Zero;
                    }
                }
            }
            return heroes;
        }

        public Dictionary<string, TimeSpan> GetHeroTimesCompetitive()
        {
            RefreshPage();
            Dictionary<string, TimeSpan> heroes = new Dictionary<string, TimeSpan>();
            HtmlNode comparisons = CachedPage.DocumentNode.SelectSingleNode(".//div[@id='competitive-play']/section/div/div[@data-group-id='comparisons']");
            foreach (HtmlNode node in comparisons.ChildNodes)
            {
                HtmlNode heroNode = node.LastChild.LastChild;
                string name = heroNode.FirstChild.InnerText;
                if (!heroes.ContainsKey(name))
                {
                    string time = heroNode.ChildNodes[1].InnerText.ToLower();
                    if (time.Contains("minute"))
                    {
                        TimeSpan timeS = TimeSpan.FromMinutes(int.Parse(time.Split(' ')[0]));

                        heroes[name] = timeS;
                    }
                    else if (time.Contains("hour"))
                    {
                        TimeSpan timeS = TimeSpan.FromHours(int.Parse(time.Split(' ')[0]));
                        heroes[name] = timeS;
                    }
                    else
                    {
                        heroes[name] = TimeSpan.Zero;
                    }
                }
            }
            return heroes;
        }
    }
}
