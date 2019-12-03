using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StopWords.Utils
{
    public static class StopWords
    {

        static ConcurrentDictionary<string, bool> funNLP_data_sensitive = new ConcurrentDictionary<string, bool>();
        static ConcurrentDictionary<int, string> replaceNewValue = new ConcurrentDictionary<int, string>();

        private const string KeywordsPath = "_Illegal/IllegalKeywords.txt";
        private const string UrlsPath = "_Illegal/IllegalUrls.txt";
//        #region static stop words
//        static string _staticStopWords = @"兼职
//招聘
//网络
//QQ
//招聘
//有意者";
//        #endregion

        static StopWords()
        {
            //LoadDataFromText(_staticStopWords);
            LoadDataFromFile();
        }

        public static void LoadDataFromFile()
        {
            string words1 = File.ReadAllText(Path.GetFullPath(KeywordsPath), Encoding.UTF8);
            string words2 = File.ReadAllText(Path.GetFullPath(UrlsPath), Encoding.UTF8);
            LoadDataFromText(words1);
            LoadDataFromText(words2);
        }

        public static void LoadDataFromFunNLP()
        {
            LoadDataFromUrl("https://github.com/fighting41love/funNLP/raw/master/data/%E6%95%8F%E6%84%9F%E8%AF%8D%E5%BA%93/%E5%B9%BF%E5%91%8A.txt");
            LoadDataFromUrl("https://github.com/fighting41love/funNLP/raw/master/data/%E6%95%8F%E6%84%9F%E8%AF%8D%E5%BA%93/%E6%94%BF%E6%B2%BB%E7%B1%BB-%E5%8F%8D%E5%8A%A8%E8%AF%8D%E5%BA%93.txt");
            LoadDataFromUrl("https://github.com/fighting41love/funNLP/raw/master/data/%E6%95%8F%E6%84%9F%E8%AF%8D%E5%BA%93/%E6%94%BF%E6%B2%BB%E7%B1%BB.txt");
            LoadDataFromUrl("https://github.com/fighting41love/funNLP/raw/master/data/%E6%95%8F%E6%84%9F%E8%AF%8D%E5%BA%93/%E6%95%8F%E6%84%9F%E8%AF%8D.txt");
            LoadDataFromUrl("https://github.com/fighting41love/funNLP/raw/master/data/%E6%95%8F%E6%84%9F%E8%AF%8D%E5%BA%93/%E6%9A%B4%E6%81%90%E8%AF%8D%E5%BA%93.txt");
            LoadDataFromUrl("https://github.com/fighting41love/funNLP/raw/master/data/%E6%95%8F%E6%84%9F%E8%AF%8D%E5%BA%93/%E6%B0%91%E7%94%9F%E8%AF%8D%E5%BA%93.txt");
            LoadDataFromUrl("https://github.com/fighting41love/funNLP/raw/master/data/%E6%95%8F%E6%84%9F%E8%AF%8D%E5%BA%93/%E6%B6%89%E6%9E%AA%E6%B6%89%E7%88%86%E8%BF%9D%E6%B3%95%E4%BF%A1%E6%81%AF%E5%85%B3%E9%94%AE%E8%AF%8D.txt");
            LoadDataFromUrl("https://github.com/fighting41love/funNLP/raw/master/data/%E6%95%8F%E6%84%9F%E8%AF%8D%E5%BA%93/%E8%89%B2%E6%83%85%E8%AF%8D%E5%BA%93.txt");

            Console.WriteLine($"敏感词文件加载完毕，总数量：{funNLP_data_sensitive.Count}");
        }

        public static void LoadDataFromText(string text)
        {
            var oldcount = funNLP_data_sensitive.Count;
            foreach (var wd in text.Split('\n'))
            {
                string keykey = wd.Trim().Trim('\r', '\n').Trim();
                if (string.IsNullOrEmpty(keykey)) continue;
                funNLP_data_sensitive.TryAdd(keykey, true);
                if (replaceNewValue.ContainsKey(keykey.Length) == false)
                    replaceNewValue.TryAdd(keykey.Length, "".PadRight(keykey.Length, '*'));
            }
            Console.WriteLine($"敏感词加载完毕，增加数量：{funNLP_data_sensitive.Count - oldcount}");
        }

        public static void LoadDataFromUrl(string url)
        {
            Console.WriteLine($"正在加载敏感词：{url}");
            var oldcount = funNLP_data_sensitive.Count;
            using (var hr = new TcpClientHttpRequest())
            {
                hr.Action = url;
                hr.Send();
                LoadDataFromText(hr.Response.Xml);
            }
        }

        /// <summary>
        /// 替换所有敏感词为 *
        /// </summary>
        /// <param name="that"></param>
        /// <returns></returns>
        public static string ReplaceStopWords(this string that)
        {
            foreach (var wd in funNLP_data_sensitive.Keys)
                that = that.Replace(wd, replaceNewValue.TryGetValue(wd.Length, out var tryval) ? tryval : "".PadRight(wd.Length, '*'));
            return that;
        }
    }
}
