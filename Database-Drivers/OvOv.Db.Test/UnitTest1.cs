using Dapper;
using System;
using System.Collections.Generic;
using Xunit;
using static Dapper.SqlBuilder;

namespace OvOv.Db.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"Title","我的名字"}
            };
            SqlBuilder builder = new SqlBuilder();

            string query = $"SELECT * FROM Post /**where**/ ";
            Template template = builder.AddTemplate(query);

            DynamicParameters dynamicParams = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                builder.Where($"{parameter.Key} = @{parameter.Key}");
                dynamicParams.Add(parameter.Key, parameter.Value);
            }

            string rawSql = template.RawSql;
        }

        [Fact]
        public void Test2()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"Title","我的名字"}
            };
            SqlBuilder builder = new SqlBuilder();

            string query = $"SELECT * FROM Post /**where**/ ";
            Template template = builder.AddTemplate(query);

            DynamicParameters dynamicParams = new DynamicParameters();
            foreach (var parameter in parameters)
            {
                builder.Where($"{parameter.Key} = @{parameter.Key}");
                dynamicParams.Add(parameter.Key, parameter.Value);
            }

            string rawSql = template.RawSql;
        }
    }
}
