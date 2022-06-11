using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qiniu.Web.Models
{
    public class ResultDto
    {
        public ResultDto(int code,string msg, object data)
        {
            Code = code;
            Data = data;
            Msg = msg;
        }

        public ResultDto(int code, string msg)
        {
            Code = code;
            Msg = msg;
        }

        public int Code { get; set; }
        public object Data { get; set; }
        public string Msg { get; set; }
    }
}
