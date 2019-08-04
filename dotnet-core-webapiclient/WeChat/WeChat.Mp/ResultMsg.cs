using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeChat.Mp
{
    public class ResultMsg
    {
        public string openid { get; set; }
        public string session_key { get; set; }
        public string unionid { get; set; }
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }
}
