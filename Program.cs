using HtmlAgilityPack;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ServiceProcess;

namespace WebScrapping
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var service = new ScrapService())
            {
                //service.ScrapData();
                service.CrawlDataByMoreButton();
                
            }

        }




        #region agiliy 
        //private void UserData()
        //{
        //    HtmlWeb web = new HtmlWeb();
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    HtmlDocument DOM = new HtmlDocument();
        //    string htmlCode = "";
        //    using (WebClient client = new WebClient())
        //    {
        //        client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
        //        htmlCode = client.DownloadString("http://localhost:51600/jQueryCRUD/GetUsersLists");
        //    }
        //    var dataResult = JsonConvert.DeserializeObject<List<User>>(htmlCode);
        //------------------------------------------------------------------------------
    //     foreach (var item in rows)
    //            {
    //                if (item.InnerText.Trim() != "")
    //                {
    //                    var ContestGroup = item.SelectNodes("th");
    //    var MatchGroup = item.SelectNodes("td/a");

    //                    if (ContestGroup != null)
    //                    {
    //                        Console.WriteLine("--------------------------------------------------------------------------------------------------");
    //                        var netNode = ContestGroup[0].InnerText;
    //    Console.WriteLine("=> " + i + ")" + netNode);
    //                        Console.WriteLine("Home Team " + "|" + " " + "Time" + "|" + " " + "Away Team");


    //                        i++;
    //                    }
    //                    else if (MatchGroup != null)
    //                    {

    //                        var match = MatchGroup[0].InnerText.Trim() + "|" + " " + MatchGroup[1].InnerText.Trim() + "|" + " " + MatchGroup[2].InnerText.Trim();
    //Console.WriteLine(match);
    //                    }
    //                }
                    
    //            }
 //------------------------------------------------------------------------------
        //}
        //private void Practice()
        //{
        //    HtmlWeb web = new HtmlWeb();
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    HtmlDocument DOM = new HtmlDocument();
        //    string htmlCode = "";
        //    //using (WebClient client = new WebClient())
        //    //{
        //    //    client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
        //    //    htmlCode = client.DownloadString("http://www.sismologia.cl/links/ultimos_sismos.html");
        //    //}
        //    //using (WebClient client = new WebClient())
        //    //{
        //    //    client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
        //    //    htmlCode = client.DownloadString("http://localhost:51600/jQueryCRUD/UsersList");
        //    //}
        //    //using (WebClient client = new WebClient())
        //    //{
        //    //    client.Headers.Add(HttpRequestHeader.UserAgent, "AvoidError");
        //    //    htmlCode = client.DownloadString("https://www.stats24.com/football");
        //    //}

        //    DOM.LoadHtml(htmlCode);
        //    var content = DOM.DocumentNode.SelectNodes("//div[@class='top_ten_match_content']/table");
        //    Console.WriteLine("Name" + " " + "<->" + " " + "Email" + " " + "<->" + " " + "Password");
        //    Console.WriteLine("---------------------------------------------------");

        //    foreach (var item in content)
        //    {
        //        var nodes = item.SelectNodes("td");
        //        if (nodes != null)
        //        {
        //            var netNode = nodes[0].InnerText + " " + "<->" + " " + nodes[1].InnerText + " " + "<->" + " " + nodes[2].InnerText;
        //            Console.WriteLine("=> " + netNode);
        //            Console.WriteLine("--------------------------------------------------------------------------------------------------");
        //        }
        //        //var nodes = item.SelectNodes("td");
        //        //var netNode = nodes[0].InnerText + "-" + nodes[1].InnerText + "-" + nodes[2].InnerText;
        //        //Console.WriteLine("=> " + netNode);
        //        //Console.WriteLine("--------------------------------------------------------------------------------------------------");
        //    }

        //    //DOM.LoadHtml(@"<tbody id='tMasterbody'><tr><td> <i class='fa fa - check - circle'></i> Mukarram Javid</td><td>mukarram_javid@yahoo.com</td><td>123</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> hashmi</td><td>cs16a649@gmail.com</td><td>123</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> Halianz</td><td>halianz059@gmail.com</td><td>213</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> <i class='fa fa - check - circle'></i> Bilal</td><td>bilalbinzia1010@gmail.com</td><td>03241014921</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> Mukarram</td><td>2016cs649@student.uet.edu.pk</td><td>03238883647</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> hamza bashir</td><td>hamzabashir902@gmail.com</td><td>03030165014</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> <i class='fa fa - check - circle'></i> Irfan Haider</td><td>szmalik4748@gmail.com</td><td>03360421626</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> <i class= fa fa - check - circle></i> Imad Khurram</td><td>mukarramjavid@gmail.com</td><td>03091483841</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> <i class=fa fa - check - circle></i> Aqeel Ahmad </td><td>2017CS680@student.uet.edu.pk</td><td>03121456084</td><td>123</td><td>Pakistan</td><td>Punjab</td></tr><tr><td> <i class='fa fa - check - circle'></i> Faizan Zafar</td><td>2016CS625@student.uet.edu.pk</td><td>ICanSurviveWithYou</td><td>MazaqSamajLytyHo</td><td>India</td><td>Dehli</td></tr></tbody>");

        //    //HtmlAgilityPack.HtmlDocument DOM = web.Load("https://mukarramblog.netlify.app/react-base-projects/");
        //    //var headerNames = DOM.DocumentNode.SelectNodes("//*[@id='tMasterbody']//tr").ToList();
        //    //foreach (HtmlNode table in headerNames)
        //    //{
        //    //    Console.WriteLine("Found: " + table.Id);
        //    //    foreach (HtmlNode tr in table.SelectNodes("tr"))
        //    //    {
        //    //        Console.WriteLine("tr: " + tr);
        //    //        foreach (var item in tr.SelectNodes("td|th"))
        //    //        {
        //    //            Console.WriteLine("cell: " + item.InnerText);
        //    //        }
        //    //    }
        //    //    foreach (HtmlNode row in table.SelectNodes("tbody"))
        //    //    {
        //    //        Console.WriteLine("row");
        //    //        //foreach (HtmlNode tr in row.SelectNodes("tr"))
        //    //        //{
        //    //        //    Console.WriteLine("tr: " + tr);
        //    //        //    foreach (var item in tr.SelectNodes("td|th"))
        //    //        //    {
        //    //        //        Console.WriteLine("cell: " + item.InnerText);
        //    //        //    }
        //    //        //}
        //    //    }
        //    //}
        //    //foreach (var item in DOM.DocumentNode.SelectNodes("//*[@id='tMasterbody']//tr"))
        //    //{
        //    //    var nodes = item.SelectNodes("td");
        //    //    var netNode = nodes[0].InnerText + "-" + nodes[1].InnerText + "-" + nodes[2].InnerText + "-" + nodes[3].InnerText + "-" + nodes[4].InnerText + "-" + nodes[5].InnerText;
        //    //    Console.WriteLine("=> " + netNode);
        //    //    Console.WriteLine("--------------------------------------------------------------------------------------------------");


        //    //}
        //    //foreach (var item in headerNames)
        //    //{
        //    //    //var hrefValue = (item.Attributes["href"].Value);
        //    //    //long data = Convert.ToInt64(hrefValue.Split('=')[1]);
        //    //    Console.WriteLine("=> " + item);
        //    //}
        //}
        #endregion
    }
}
