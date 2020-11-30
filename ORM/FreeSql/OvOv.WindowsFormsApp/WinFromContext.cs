using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp1
{
    public class WinFromContext : DbContext
    {
        public DbSet<ConfigInfo> ConfigInfo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseFreeSql(DB.Sqlite);
            //base.OnConfiguring(options);
        }
    }
}
