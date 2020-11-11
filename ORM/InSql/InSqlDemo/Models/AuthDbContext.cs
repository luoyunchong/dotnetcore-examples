using Insql;
using System.Linq;

namespace InSqlDemo.Models
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        public UserInfo GetUser(int userId)
        {
            //第一个参数与select id 对应，第二个数据参数支持PlainObject和IDictionary<string,object>类型
            return Query<UserInfo>(nameof(GetUser), new { userId }).SingleOrDefault();
        }

        public void InsertUserSelective(UserInfo userinfo)
        {
             Execute(nameof(InsertUserSelective),userinfo);
        }
    }
}
