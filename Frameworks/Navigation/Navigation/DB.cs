using System;
using System.Diagnostics;
using System.Threading;

namespace Navigation
{
    public class DB
    {
        static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() => new FreeSql.FreeSqlBuilder()
             .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=|DataDirectory|\navigation.db;")
             //.UseConnectionFactory(FreeSql.DataType.Sqlite, () =>
             //{
             //    var conn = new System.Data.SQLite.SQLiteConnection(@"Data Source=|DataDirectory|\document.db;Pooling=true;");
             //    //conn.Open();
             //    //var cmd = conn.CreateCommand();
             //    //cmd.CommandText = $"attach database [xxxtb.db] as [xxxtb];\r\n";
             //    //cmd.ExecuteNonQuery();
             //    //cmd.Dispose();
             //    return conn;
             //})
             .UseAutoSyncStructure(true)
             //.UseGenerateCommandParameterWithLambda(true)
             .UseLazyLoading(true)
             .UseMonitorCommand(
                 cmd => Trace.WriteLine("\r\n线程" + Thread.CurrentThread.ManagedThreadId + ": " + cmd.CommandText) //监听SQL命令对象，在执行前
                                                                                                                  //, (cmd, traceLog) => Console.WriteLine(traceLog)
                 )
             .Build());

        public static IFreeSql Sqlite => sqliteLazy.Value;

    }
}
