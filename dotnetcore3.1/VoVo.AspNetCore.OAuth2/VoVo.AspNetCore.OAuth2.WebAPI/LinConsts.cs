using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoVo.AspNetCore.OAuth2.WebAPI
{
    public static class LinConsts
    {
        public static class Claims
        {
            public const string BIO = "urn:github:bio";
            public const string AvatarUrl = "urn:github:avatar_url";
            public const string BlogAddress = "urn:github:blog";
        }
    }
}
