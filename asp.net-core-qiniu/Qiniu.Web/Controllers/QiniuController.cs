using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;

namespace Qiniu.Web.Controllers
{
    /// <summary>
    /// 七牛云上传服务
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QiniuController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public QiniuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 根据后台配置项，得到请求七牛云的token值，前台也可根据此token值上传至七牛云服务
        /// </summary>
        /// <returns></returns>
        [HttpGet("access_token")]
        public string GetAccessToken()
        {
            Mac mac = new Mac(_configuration["Qiniu:AK"], _configuration["Qiniu:SK"]);
            PutPolicy putPolicy = new PutPolicy { Scope = _configuration["Qiniu:Bucket"] };
            return Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
        }

        /// <summary>
        /// 上传文件至七牛云,code为200，代表上传成功,其他代表不成功
        /// </summary>
        /// <param name="file">单个文件</param>
        /// <returns>new { code = 200, data ="七牛云文件地址，包括http://....mm.png", msg = "上传成功" };</returns>
        [HttpPost("upload")]
        public dynamic Upload(IFormFile file)
        {
            if (file.Length == 0)
            {
                return new { code = 1, msg = "文件为空" };
            }

            FormUploader upload = new FormUploader(new Config()
            {
                Zone = Zone.ZONE_CN_South,//华南 
                UseHttps = true
            });

            var fileName = ContentDispositionHeaderValue
                .Parse(file.ContentDisposition)
                .FileName.Trim();

            string qiniuName = _configuration["Qiniu:PrefixPath"] + "/" + DateTime.Now.ToString("yyyyMMddHHmmssffffff") + fileName;
            Stream stream = file.OpenReadStream();
            HttpResult result = upload.UploadStream(stream, qiniuName, GetAccessToken(), null);


            if (result.Code == 200)
            {
                return new { code = 200, data =_configuration["Qiniu:Host"]+ qiniuName, msg = "上传成功" };
            }

            return new { code = 1, msg = "上传失败" };
        }
    }

}

//https://www.cnblogs.com/OMango/p/8447480.html
//https://github.com/Hello-Mango/MQiniu.Core