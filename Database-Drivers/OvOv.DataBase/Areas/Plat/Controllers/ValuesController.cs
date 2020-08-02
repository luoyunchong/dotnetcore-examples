using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DataBase.Areas.Plat.Controllers
{
    [Area("v1")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //更新northwind数据库的Employees表
                    using (MySqlConnection conOne = new MySqlConnection("server=127.0.0.1;user id=root;password=123456;port=3306;database=pub-1;"))
                    {
                        conOne.Open();
                        MySqlCommand command = new MySqlCommand("insert into jobs (name,create_time)values('123',now())", conOne);
                        int i = command.ExecuteNonQuery();
                    }
                    //更新pubs数据库的jobs表
                    using (MySqlConnection conTwo = new MySqlConnection("server=127.0.0.1;user id=root;password=123456;port=3306;database=pub-2;"))
                    {
                        conTwo.Open();
                        MySqlCommand command = new MySqlCommand("insert into jobs (name,create_time)values('123',now())", conTwo);
                        int i = command.ExecuteNonQuery();
                    }
                    scope.Complete();  //提交事物
                }
            }
            catch (Exception ex)       //发生异常后自动回滚
            {
                throw;
            }
            return "ok";
        }

        [HttpGet("2")]
        public string Get2()
        {
            MySqlConnection conNorthwind = new MySqlConnection("server=127.0.0.1;user id=root;password=123456;port=3306;database=pub-1;");
            MySqlConnection conPubs = new MySqlConnection("server=127.0.0.1;user id=root;password=123456;port=3306;database=pub-2;");
            MySqlCommand commandNorthwind = new MySqlCommand();
            MySqlCommand commandPubs = new MySqlCommand();
            try
            {
                conNorthwind.Open();
                conPubs.Open();
                //更新northwind数据库的Employees表
                MySqlTransaction tranNorthwind = conNorthwind.BeginTransaction();
                commandNorthwind.Connection = conNorthwind;
                commandNorthwind.Transaction = tranNorthwind;
                commandNorthwind.CommandText = "insert into jobs (name,create_time)values('123',now())";
                int i = commandNorthwind.ExecuteNonQuery();
                //更新pubs数据库的jobs表
                MySqlTransaction tranPubs = conPubs.BeginTransaction();
                commandPubs.Connection = conPubs;
                commandPubs.Transaction = tranPubs;
                commandPubs.CommandText = "insert into jobs (name,create_time)values('123',now())";
                int k = commandPubs.ExecuteNonQuery();
                //throw new Exception();
                //提交事务
                commandNorthwind.Transaction.Commit();
                conNorthwind.Close();
                commandPubs.Transaction.Commit();
                conPubs.Close();
            }
            catch (Exception ex)
            {
                //回滚事务
                if (commandNorthwind.Transaction != null && conNorthwind != null)
                {
                    commandNorthwind.Transaction.Rollback();
                    conNorthwind.Close();
                }
                if (commandPubs.Transaction != null && conPubs != null)
                {
                    commandPubs.Transaction.Rollback();
                    conPubs.Close();
                }
                throw;
            }
            return "ok";
        }
    }
}
