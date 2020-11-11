using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navigation
{
    public class NavigationHelper
    {

        public static void FromWeb()
        {
            var html = @"https://www.chenzhuofan.top/v2";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDoc = web.Load(html);
            HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='测试专家']");

            Console.WriteLine("Node Name: " + node.Name + "\n" + node.OuterHtml);
        }


        /// <summary>
        /// 解析 https://www.chenzhuofan.top/v2
        /// </summary>
        /// <returns></returns>
        public static  List<NavigationLink> ParseChenZhouFanV2()
        {
            var url = "https://www.chenzhuofan.top/v2";
            HtmlWeb web = new HtmlWeb();
            //1.支持从web或本地path加载html
            var htmlDoc = web.Load(url);
            var post_listnode = htmlDoc.DocumentNode.SelectSingleNode("//main[@class='main']");
            //Console.WriteLine("Node Name: " + post_listnode.Name + "\n" + post_listnode.OuterHtml);
            var postitemsNodes = post_listnode.SelectNodes("//div[@class='col-sm-6 col-md-4 col-lg-3']");
            var navigationLinks = new List<NavigationLink>();
            foreach (var item in postitemsNodes)
            {
                var navigationLink = new NavigationLink();
                //body
                var logUrl = item.SelectSingleNode(item.XPath + "//img[@class='img-responsive']");
                var Url = item.SelectSingleNode(item.XPath + "//a[@target='_blank']");
                var Name = item.SelectSingleNode(item.XPath + "//span[@class='web-name']");
                var Content = item.SelectSingleNode(item.XPath + "//div[@class='dot web-content-bottom-content']");

                navigationLink.Url = Url.Attributes["href"].Value;

                if (!(navigationLink.Url.StartsWith("http") || navigationLink.Url.StartsWith("https")))
                {
                    navigationLink.Url = url + navigationLink.Url;
                }
                //navigationLink.Id = Guid.NewGuid();
                navigationLink.LogUrl = logUrl.Attributes["src"].Value; ;
                navigationLink.Name = Name.InnerText;
                navigationLink.Content = Content.InnerText;

                //Console.WriteLine(item.InnerHtml);
                navigationLinks.Add(navigationLink);
            }
            return navigationLinks;
        }

    }
}
