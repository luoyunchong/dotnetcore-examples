using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeSql;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OvOv.FreeSql.IdleBus.Controllers
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
        private readonly IFreeSql fsql;
        private readonly IBaseRepository<WeatherForecast> repository;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IFreeSql fsql, IBaseRepository<WeatherForecast> repository)
        {
            _logger = logger;
            this.fsql = fsql;
            this.repository = repository;
        }

        [HttpGet("change")]
        public List<WeatherForecast> Get2()
        {
            var db1_list = fsql.Select<WeatherForecast>().ToList();
            //切换 db2 数据库，一旦切换之后 fsql 操作都是针对 db2
            fsql.Change("db2");
            var db2_list = fsql.Select<WeatherForecast>().ToList();

            //这样也行
            var db2_list2 = fsql.Change("db2").Select<WeatherForecast>().ToList();

            return db2_list;
        }

        [HttpGet]
        public List<WeatherForecast> Get1(string db="db1")
        {
            fsql.Change(db);

            var d=repository.Select.ToList();

            return d;
        }
    }
}
