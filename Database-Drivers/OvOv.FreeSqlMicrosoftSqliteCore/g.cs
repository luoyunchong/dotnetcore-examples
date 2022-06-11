using System.Diagnostics;

public class g
{
    static Lazy<IFreeSql> sqliteLazy = new Lazy<IFreeSql>(() =>
    {
        //string dataSubDirectory = Path.Combine(AppContext.BaseDirectory);

        //if (!Directory.Exists(dataSubDirectory))
        //    Directory.CreateDirectory(dataSubDirectory);

        //AppDomain.CurrentDomain.SetData("DataDirectory", dataSubDirectory);

        var fsql = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=local.db;Password=123qwe")
                //.UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=local.db")
                //.UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=|DataDirectory|local.db;Password=123qwe")
                .UseAutoSyncStructure(true)
                .UseLazyLoading(true)
                .UseMonitorCommand(
                    cmd => Trace.WriteLine("\r\n线程" + Thread.CurrentThread.ManagedThreadId + ": " + cmd.CommandText)
                    )
                .Build();

        return fsql;
    }
   );
    public static IFreeSql sqlite => sqliteLazy.Value;
}


