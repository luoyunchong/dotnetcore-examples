using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace NacosApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
    private readonly ILogger<ConfigController> _logger;
    private readonly IConfiguration _configuration;

    private readonly UserInfo _user1;
    private readonly UserInfo _user2;
    private readonly UserInfo _user3;

    public ConfigController(ILogger<ConfigController> logger,
        IConfiguration configuration,
            IOptions<UserInfo> options1,
            IOptionsSnapshot<UserInfo> options2,
            IOptionsMonitor<UserInfo> options3
        )
    {
        _logger = logger;
        _configuration = configuration;
        _user1 = options1.Value;
        _user2 = options2.Value;
        _user3 = options3.CurrentValue;
    }

    [HttpGet("getconfig")]
    public UserInfo GetConfig()
    {
        var userInfo1 = _configuration.GetSection("UserInfo").Get<UserInfo>();
        var commonvalue = _configuration["commonkey"];
        var demovalue = _configuration["demokey"];
        _logger.LogInformation("commonkey:" + commonvalue);
        _logger.LogInformation("demokey:" + demovalue);
        return userInfo1;
    }

    [HttpGet]
    public string Get()
    {
        string id = Guid.NewGuid().ToString("N");

        _logger.LogInformation($"============== begin {id} =====================");

        var str1 = Newtonsoft.Json.JsonConvert.SerializeObject(_user1);
        _logger.LogInformation($"{id} IOptions = {str1}");

        var str2 = Newtonsoft.Json.JsonConvert.SerializeObject(_user2);
        _logger.LogInformation($"{id} IOptionsSnapshot = {str2}");

        var str3 = Newtonsoft.Json.JsonConvert.SerializeObject(_user3);
        _logger.LogInformation($"{id} IOptionsMonitor = {str3}");

        _logger.LogInformation($"===============================================");

        return "ok";
    }
}
