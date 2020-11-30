using FreeSql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace WindowsFormsApp1
{
    public class DB
    {
        static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder()
            //.UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=|DataDirectory|\DispatchProxy.db;")
            .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=d:\db\DispatchProxy.db;")
            .UseAutoSyncStructure(true)
            .UseLazyLoading(true)
            .UseNoneCommandParameter(true)
            .UseMonitorCommand(
                cmd => Trace.WriteLine("\r\n线程" + Thread.CurrentThread.ManagedThreadId + ": " + cmd.CommandText) //监听SQL命令对象，在执行前
                                                                                                                 //, (cmd, traceLog) => Console.WriteLine(traceLog)
                )
            .Build());


        static Lazy<IFreeSql> mysqlLazy = new Lazy<IFreeSql>(() => new FreeSqlBuilder()
             .UseConnectionString(FreeSql.DataType.MySql, @"Data Source=localhost;Port=3306;User ID=root;Password=root;Initial Catalog=DispatchProxy;Charset=utf8;SslMode=none;Max pool size=1;Connection LifeTime=20")
             .UseAutoSyncStructure(true)
             .UseLazyLoading(true)
             .UseMonitorCommand(
                 cmd => Trace.WriteLine("\r\n线程" + Thread.CurrentThread.ManagedThreadId + ": " + cmd.CommandText) //监听SQL命令对象，在执行前
                                                                                                                  //, (cmd, traceLog) => Console.WriteLine(traceLog)
                 )
             .Build());


        public static IFreeSql Sqlite => sqliteLazy.Value;
        public static IFreeSql MySql => mysqlLazy.Value;

    }
}
