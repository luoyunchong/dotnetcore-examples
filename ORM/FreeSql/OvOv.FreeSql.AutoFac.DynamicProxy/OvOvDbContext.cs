using FreeSql;
using OvOv.Core.Domain;

namespace OvOv.FreeSql.AutoFac.DynamicProxy
{
    public class OvOvDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
        }

        protected override void OnModelCreating(ICodeFirst codefirst)
        {
            codefirst.Entity<Blog>(r =>
            {
                r.Property(r => r.Version).IsRowVersion().Help().MapType(typeof(byte[]));
            });
        }

    }
}
