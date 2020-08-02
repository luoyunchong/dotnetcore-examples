using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.Words;

namespace StopWords.Utils
{
    /// <summary>
    /// ToolGood.Words类库配合敏感库
    /// </summary>
    public class ToolGoodUtils
    {
        //敏感库只要这二个文件存在即可
        private const string KeywordsPath = "_Illegal/IllegalKeywords.txt";
        private const string UrlsPath = "_Illegal/IllegalUrls.txt";

        private const string InfoPath = "_Illegal/IllegalInfo.txt";
        private const string BitPath = "_Illegal/IllegalBit.iws";

        private static IllegalWordsSearch _search;
        public static IllegalWordsSearch GetIllegalWordsSearch()
        {
            if (_search == null)
            {
                string ipath = Path.GetFullPath(InfoPath);
                if (File.Exists(ipath) == false)
                {
                    _search = CreateIllegalWordsSearch();
                }
                else
                {
                    var texts = File.ReadAllText(ipath).Split('|');
                    if (new FileInfo(Path.GetFullPath(KeywordsPath)).LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") !=
                        texts[0] ||
                        new FileInfo(Path.GetFullPath(UrlsPath)).LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") !=
                        texts[1]
                    )
                    {
                        _search = CreateIllegalWordsSearch();
                    }
                    else
                    {
                        var s = new IllegalWordsSearch();
                        s.Load(Path.GetFullPath(BitPath));
                        _search = s;
                    }
                }
            }
            return _search;
        }

        private static IllegalWordsSearch CreateIllegalWordsSearch()
        {
            string[] words1 = File.ReadAllLines(Path.GetFullPath(KeywordsPath), Encoding.UTF8);
            string[] words2 = File.ReadAllLines(Path.GetFullPath(UrlsPath), Encoding.UTF8);
            var words = new List<string>();
            foreach (var item in words1)
            {
                words.Add(item.Trim());
            }
            foreach (var item in words2)
            {
                words.Add(item.Trim());
            }

            var search = new IllegalWordsSearch();
            search.SetKeywords(words);

            search.Save(Path.GetFullPath(BitPath));

            var text = new FileInfo(Path.GetFullPath(KeywordsPath)).LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss") + "|"
                                                                                                                  + new FileInfo(Path.GetFullPath(UrlsPath)).LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
            File.WriteAllText(Path.GetFullPath(InfoPath), text);

            return search;
        }

    }
}
