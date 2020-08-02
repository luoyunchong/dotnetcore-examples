using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StopWords.Utils;
using ToolGood.Words;

namespace StopWords.Controllers
{
    [Route("api/word")]
    [ApiController]
    public class WordController : ControllerBase
    {
        /// <summary>
        /// 敏感词替换-https://github.com/toolgood/ToolGood.Words
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpGet]
        public string Get(string text)
        {
            var search = ToolGoodUtils.GetIllegalWordsSearch();
            return search.Replace(text);
        }

        /// <summary>
        /// 敏感词替换-FreeSql大佬写的
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpGet("replace")]
        public string GetWord(string text)
        {
            return text.ReplaceStopWords();
        }


        [HttpGet("find")]
        public static List<IllegalWordsSearchResult> FindAll(string text)
        {
            var search = ToolGoodUtils.GetIllegalWordsSearch();
            return search.FindAll(text);
        }

    }
}
