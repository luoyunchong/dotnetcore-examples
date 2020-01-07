using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace Cap.FreeSql.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IFreeSql _freesql;

        private readonly ICapPublisher _capPublisher;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICapPublisher capPublisher, IFreeSql freesql)
        {
            _logger = logger;
            _capPublisher = capPublisher;
            _freesql = freesql;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();


            using (var connection = new MySqlConnection("Data Source=localhost;Port=3306;User ID=root;Password=123456;Initial Catalog=LinCms-CAP;Charset=utf8mb4;SslMode=none;Max pool size=10"))
            {
                using var transaction = connection.BeginTransaction(_capPublisher, autoCommit: true);
                //业务代码
                connection.Execute("insert into test(name) values('test')", transaction: (IDbTransaction)transaction.DbTransaction);
                _capPublisher.Publish("xxx.services.show.time", result);
            }

            return result;
        }

        [CapSubscribe("xxx.services.show.time")]
        public void GetResult(List<WeatherForecast> weatherForecasts)
        {
            Console.WriteLine(weatherForecasts);
        }


        [HttpGet("time")]
        public DateTime GetTime()
        {

            DateTime now = DateTime.Now;


            using (var dbContext = _freesql.CreateDbContext())
            {
                using (var trans=_freesql.BeginTransaction(_capPublisher, false))
                {
                    dbContext.Add(new WeatherForecast()
                    {
                        Date = now,
                        Summary = "summary",
                        TemperatureC = 300
                    });

                    _capPublisher.Publish("time", now);

                    dbContext.Add(new WeatherForecast()
                    {
                        Date = now,
                        Summary = "summar",
                        TemperatureC = 200
                    });
                    trans.Commit();
                    dbContext.SaveChanges();
                }

            }

            return now;
        }


        [HttpGet("time2")]
        public DateTime GetTime2()
        {
            DateTime now = DateTime.Now;
            using (var uow = _freesql.CreateUnitOfWork())
            {
                var trans = _freesql.BeginTransaction(_capPublisher, false);
                var repo = uow.GetRepository<WeatherForecast>();

                repo.Insert(new WeatherForecast()
                {
                    Date = now,
                    Summary = "summary",
                    TemperatureC = 100
                });

                _capPublisher.Publish("time", now);
                repo.Insert(new WeatherForecast()
                {
                    Date = now,
                    Summary = "summarysuy",
                    TemperatureC = 200
                });

                uow.Commit();
                trans.Commit();
            }

            return now;
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <returns></returns>
        [HttpGet("time3")]
        public DateTime GetTime3()
        {
            DateTime now = DateTime.Now;
            using (var uow = _freesql.CreateUnitOfWork())
            {
                ICapTransaction trans = uow.BeginTransaction(_capPublisher, false);
                var repo = uow.GetRepository<WeatherForecast>();

                repo.Insert(new WeatherForecast()
                {
                    Date = now,
                    Summary = "summary",
                    TemperatureC = 100
                });

                _capPublisher.Publish("time", now);
                repo.Insert(new WeatherForecast()
                {
                    Date = now,
                    Summary = "summarysuy",
                    TemperatureC = 200
                });
                trans.Commit();
                //uow.Commit();
            }

            return now;
        }
        [CapSubscribe("time")]
        public void GetTime(DateTime time)
        {
            Console.WriteLine($"time:{time}");
        }
    }
}
