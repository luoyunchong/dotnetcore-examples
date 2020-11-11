using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Navigation.Test
{
    public class UnitTest1
    {
        private ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestFsqlGetHashCode()
        {
            string hashCode = DB.Sqlite.GetHashCode().ToString();
            output.WriteLine(hashCode);
            string hashCode2 = DB.Sqlite.GetHashCode().ToString();
            output.WriteLine(hashCode2);
        }

        [Fact]
        public void Test1()
        {
            Task.Run(() =>
            {
                string hashCode=DB.Sqlite.GetHashCode().ToString();
                output.WriteLine(hashCode);
            });
            Task.Run(() =>
            {
                string hashCode = DB.Sqlite.GetHashCode().ToString();
                output.WriteLine(hashCode);
            });
            Task.Run(() =>
            {
                string hashCode = DB.Sqlite.GetHashCode().ToString();
                output.WriteLine(hashCode);
            });
            Task.Run(() =>
            {
                string hashCode = DB.Sqlite.GetHashCode().ToString();
                output.WriteLine(hashCode);
            });
            Task.Run(() =>
            {
                string hashCode = DB.Sqlite.GetHashCode().ToString();
                output.WriteLine(hashCode);
            });

        }

        [Fact]
        public void ExecuteDBSave()
        {

            List<NavigationLink> navigationLinks = NavigationHelper.ParseChenZhouFanV2();

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

            Console.WriteLine("over!");
        }
    }
}
