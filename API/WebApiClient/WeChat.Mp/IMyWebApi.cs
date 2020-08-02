using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace WeChat.Mp
{
    public interface IMyWebApi : IHttpApi
    {
        [HttpGet("https://api.weixin.qq.com/sns/jscode2session")]
        [JsonReturn]
        ITask<ResultMsg> Jscode2Session(string appid,string secret,string js_code,string grant_type= "authorization_code");
    }
}
