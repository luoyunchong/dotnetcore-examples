using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Xunit;

namespace OvOv.FreeSql.xUnit
{
    public class UnitTest1
    {
        public IFreeSql _fsql;

        public UnitTest1(IFreeSql fsql)
        {
            _fsql = fsql;
        }

        [Fact]
        public string GetDataTable()
        {
            string sql = "select * from dbo.blog";

            DataTable dt = _fsql.Select<object>().WithSql(sql).Page(1, 10).ToDataTable("*");

            return "ok";
        }
    }


    public class XunitStartup : Startup
    {
        public XunitStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
