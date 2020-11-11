using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Navigation
{
    class Program
    {
        static void Main(string[] args)
        {
            //FromWeb();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<NavigationLink> navigationLinks = NavigationHelper.ParseChenZhouFanV2();

            stopwatch.Stop();
            Console.WriteLine($"获取https://www.chenzhuofan.top/v2内容，并解析，花费：{stopwatch.ElapsedMilliseconds}ms");


            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            //需要手动设置主键值Id才能正常插入
            //DB.Sqlite.InsertOrUpdate<NavigationLink>().SetSource(navigationLinks).ExecuteAffrows();

            foreach (var item in navigationLinks)
            {

                bool any = DB.Sqlite.Select<NavigationLink>().Where(r => r.Url == item.Url).Any();

                if (!any)
                {
                    DB.Sqlite.Insert(item).ExecuteAffrows();
                }
            }

            stopwatch2.Stop();
            Console.WriteLine($"存入数据库共花费：{stopwatch2.ElapsedMilliseconds}ms");
        }


    }
}
