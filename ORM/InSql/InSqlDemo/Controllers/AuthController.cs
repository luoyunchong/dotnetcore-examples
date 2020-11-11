using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InSqlDemo.Models;
using Insql;

namespace InSqlDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthDbContext dbContext;

        public AuthController(AuthDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public UserInfo GetUser(int userId)
        {
            return this.dbContext.GetUser(userId);
        }

        [HttpGet]
        public IEnumerable<RoleInfo> GetRoleList()
        {
            return this.dbContext.Query<RoleInfo>("GetRoleList");
        }
        public void InsertUserList(IEnumerable<UserInfo> list)
        {
            try
            {
                this.dbContext.BeginTransaction();

                foreach (var item in list)
                {
                    this.dbContext.InsertUserSelective(item);
                }

                this.dbContext.CommitTransaction();
            }
            catch
            {
                this.dbContext.RollbackTransaction();

                throw;
            }
        }
    }
}
