using AngleSharp.Html.Dom;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Html;
using ScrapySharp.Html.Forms;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using WebScrapping.SoccorRepository;

namespace WebScrapping
{
    public class ScrapService : ServiceBase
    {
        public ScrapService()
        {
            ServiceName = "ScrapingService";
        }
        public void ScrapData()
        {
            try
            {
                int i = 1;
                HtmlDocument DOM = new HtmlDocument();
                string url = ConfigurationManager.AppSettings["SiteUrl"];
                var data = WebClient(url);
                DOM.LoadHtml(data);
                var rows = DOM.DocumentNode.SelectNodes("//table//tbody");
                foreach (var item in rows)
                {
                    if (item.InnerText.Trim() != "")
                    {
                        var ContestGroup = item.SelectNodes("tr[starts-with(@class,'group-head')]");
                        var MatchGroup = item.SelectNodes("tr");
                        foreach (var cg in ContestGroup)
                        {
                            var contestgroupname = cg.InnerText.Trim().Split('\n')[0];
                            Console.WriteLine("=> " + i + ")" + contestgroupname);
                            if (cg.InnerText.Trim() != "")
                            {
                                var cId = cg.Attributes["id"].Value;
                                var MatchingId = cId.Split('-')[1];

                                foreach (var matches in MatchGroup)
                                {
                                    if (matches.Attributes["data-competition"] != null)
                                    {
                                        var mId = matches.Attributes["data-competition"].Value;
                                        if (MatchingId == mId)
                                        {
                                            var _match = matches.InnerText.Trim().Split('\n');
                                            Console.WriteLine(" " + _match[3].Trim() + " " + "|" + " " + _match[10].Trim() + " " + "|" + " " + _match[16].Trim());
                                        }
                                    }
                                }
                            }
                            Console.WriteLine("--------------------------------------------------------------------------------------------------", Console.ForegroundColor = ConsoleColor.Green);
                            i++;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("--------------------------------------------------------------------------------------------------", Console.ForegroundColor = ConsoleColor.Red);
                var ex = e.Message;
            }

        }

        public void CrawlDataByMoreButton()
        {
            try
            {
                int i = 1;
                HtmlDocument DomMain = new HtmlDocument();
                string url = ConfigurationManager.AppSettings["SiteUrl"];
                var data = WebClient(url);
                DomMain.LoadHtml(data);
                var links = DomMain.DocumentNode.SelectNodes("//table//tbody//tr[starts-with(@class,'group-head')]");
                // For ContestGroupName
                foreach (var Id in links)
                {
                    var contestGroupName = Id.InnerText.Trim().Split('\n')[0];
                    var MatchingId = Id.Attributes["id"].Value.Split('-')[1];
                    var link = Id.SelectNodes("th[@class='competition-link']/a");
                    Console.WriteLine("=> " + i + ")" + contestGroupName, Console.ForegroundColor = ConsoleColor.Green);

                    // For Match Summary Link
                    foreach (var item in link)
                    {
                        if (item.InnerText.Trim() != "")
                        {
                            var l = item.Attributes["href"].Value;
                            var summary = WebClient(url + l);
                            HtmlDocument DomSub = new HtmlDocument();
                            DomSub.LoadHtml(summary);
                            var matchList = DomSub.DocumentNode.SelectNodes("//table[starts-with(@class,'matches   ')]//tbody//tr");

                            // For Upcomming Matches
                            foreach (var matches in matchList)
                            {

                                if (matches.Attributes["data-competition"] != null)
                                {
                                    var mId = matches.Attributes["data-competition"].Value;
                                    if (MatchingId == mId)
                                    {
                                        var team_a = matches.Descendants().Where(n => n.HasClass("team-a"));
                                        var team_b = matches.Descendants().Where(n => n.HasClass("team-b"));
                                        var day = matches.Descendants().Where(n => n.HasClass("day"));
                                        var matchId = matches.Descendants().Where(n => n.HasClass("score-time"));

                                        if (team_a != null && team_b != null && day != null && matchId != null)
                                        {
                                            var teamName_a = team_a.FirstOrDefault().InnerText.Trim();
                                            var teamLink_a = team_a.FirstOrDefault().SelectNodes("a");
                                            var teamName_b = team_b.FirstOrDefault().InnerText.Trim();
                                            var teamLink_b = team_b.FirstOrDefault().SelectNodes("a");

                                            var homeId = teamLink_a != null ? teamLink_a.FirstOrDefault().Attributes["href"].Value.Split('/')[4] : "0";
                                            var awayId = teamLink_b != null ? teamLink_b.FirstOrDefault().Attributes["href"].Value.Split('/')[4] : "0";
                                            var teamName_day = day.FirstOrDefault().InnerText.Trim();




                                            var MID = matchId.FirstOrDefault().SelectNodes("a").FirstOrDefault().Attributes["href"].Value;
                                            var _matchId = MID != "" ? MID.Split('/')[9] : "0";

                                            if (teamName_day != "FT")
                                            {
                                                //var ContestGroupId = DomSub.DocumentNode.SelectSingleNode("//html//head//link").Attributes["href"].Value.Split("/regular-season", StringSplitOptions.RemoveEmptyEntries)[1];
                                                //Console.WriteLine("ContestGroupId=> " + ContestGroupId, Console.ForegroundColor = ConsoleColor.Cyan);
                                                Console.WriteLine("MathcId=> " + _matchId, Console.ForegroundColor = ConsoleColor.DarkYellow);
                                                Console.WriteLine(" " + teamName_a + " " + homeId + " " + "|" + " " + teamName_day + " " + "|" + " " + teamName_b + " " + awayId, Console.ForegroundColor = ConsoleColor.Green);
                                                var soccor = new Soccor()
                                                {
                                                    MatchId = _matchId,
                                                    MatchTime = teamName_day,
                                                    ContestGroupName = contestGroupName,
                                                    HomeId = homeId,
                                                    HomeName = teamName_a,
                                                    AwayId = awayId,
                                                    AwayName = teamName_b
                                                };
                                                SoccorRepoBLL.InsertAll(soccor);


                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------------------------", Console.ForegroundColor = ConsoleColor.Green);
                    i++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("-----------" + e.Message, Console.ForegroundColor = ConsoleColor.Red);

            }
        }
        public string WebClient(string url)
        {
            string htmlCode = "";
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
                htmlCode = client.DownloadString(url);
            }
            return htmlCode;
        }
    }
}
