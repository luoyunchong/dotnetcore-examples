using FreeSql;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OvOv.FreeSql
{
    public static class FreeSqlExtensions
    {
        public static IFreeSql CreateDatabaseIfNotExists(this IFreeSql @this, DataType dbType)
        {
            switch (dbType)
            {
                case DataType.MySql:
                    return @this.CreateDatabaseIfNotExistsMySql();
                case DataType.SqlServer:
                    return @this.CreateDatabaseIfNotExistsSqlServer();
                case DataType.PostgreSQL:
                    break;
                case DataType.Oracle:
                    break;
                case DataType.Sqlite:
                    break;
                case DataType.OdbcOracle:
                    break;
                case DataType.OdbcSqlServer:
                    break;
                case DataType.OdbcMySql:
                    break;
                case DataType.OdbcPostgreSQL:
                    break;
                case DataType.Odbc:
                    break;
                case DataType.OdbcDameng:
                    break;
                case DataType.MsAccess:
                    break;
                case DataType.Dameng:
                    break;
                case DataType.OdbcKingbaseES:
                    break;
                case DataType.ShenTong:
                    break;
                case DataType.KingbaseES:
                    break;
                case DataType.Firebird:
                    break;
                default:
                    break;
            }
            throw new NotSupportedException("不支持创建数据库");
        }

        public static IFreeSql CreateDatabaseIfNotExistsMySql(this IFreeSql @this)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(@this.Ado.ConnectionString);

            string createDatabaseSQL = $"USE sys;CREATE DATABASE IF NOT EXISTS `{builder.Database}` CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_general_ci'";

            using (MySqlConnection cnn = new MySqlConnection($"Data Source={builder.Server};Port={builder.Port};User ID={builder.UserID};Password={builder.Password};Initial Catalog=mysql;Charset=utf8;SslMode=none;Max pool size=1"))
            {
                cnn.Open();
                using (MySqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = createDatabaseSQL;
                    cmd.ExecuteNonQuery();
                }
            }
            return @this;
        }


        //use master
        //go
        //create database test --创建数据库

        ///*以下创建数据库的语法可以不用记，选中CREATE DATABASE,按F1就可查看帮助文档，可
        //以直接拷贝文档中的demo语句，然后稍微修改下即可*/
        //CREATE DATABASE testDB
        //ON
        //(NAME = testDB_dat,
        //    FILENAME = 'D:\test\testDBdat.mdf', --D盘下要有test文件夹
        //    SIZE = 10,
        //    MAXSIZE = 50,
        //    FILEGROWTH = 5)
        //LOG ON
        //(NAME = testDB_log,
        //    FILENAME = 'D:\test\testDBlog.ldf', --D盘下要有test文件夹
        //    SIZE = 5MB,
        //    MAXSIZE = 25MB,
        //     FILEGROWTH = 5MB );
        //GO



        public static IFreeSql CreateDatabaseIfNotExistsSqlServer(this IFreeSql @this)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(@this.Ado.ConnectionString);
            string name;
            string createDatabaseSQL;
            if (!string.IsNullOrEmpty(builder.AttachDBFilename))
            {
                string fileName = ExpandFileName(builder.AttachDBFilename);
                name = Path.GetFileNameWithoutExtension(fileName);
                string logFileName = Path.ChangeExtension(fileName, ".ldf");
                createDatabaseSQL = @$"CREATE DATABASE {builder.InitialCatalog}   on  primary   
                (
                    name = '{name}',
                    filename = '{fileName}'
                )
                log on
                (
                    name= '{name}_log',
                    filename = '{logFileName}'
                )";
            }
            else
            {
                createDatabaseSQL = @$"CREATE DATABASE {builder.InitialCatalog}";
            }

            using (SqlConnection cnn = new SqlConnection($"Data Source={builder.DataSource};Integrated Security = True;User ID={builder.UserID};Password={builder.Password};Initial Catalog=master;Min pool size=1"))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = $"select * from sysdatabases where name = '{builder.InitialCatalog}'";

                    SqlDataAdapter apter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    apter.Fill(ds);

                    if(ds.Tables[0].Rows.Count==0)
                    {
                        cmd.CommandText = createDatabaseSQL;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            return @this;
        }

        private static string ExpandFileName(string fileName)
        {
            if (fileName.StartsWith("|DataDirectory|", StringComparison.OrdinalIgnoreCase))
            {
                var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
                if (string.IsNullOrEmpty(dataDirectory))
                {
                    dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
                }

                fileName = Path.Combine(dataDirectory, fileName.Substring("|DataDirectory|".Length));
            }

            return Path.GetFullPath(fileName);
        }


    }
}
