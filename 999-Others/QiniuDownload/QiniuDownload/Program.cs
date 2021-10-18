using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.IO;

namespace QiniuDownload
{
    public class QiniuOptions
    {
        public string AK { get; set; } = "";
        public string SK { get; set; } = "";
        public string Bucket { get; set; } = "vvv";
        public string PrefixPath { get; set; } = "";
        public string Host { get; set; } = "http://images.igeekfan.cn/";
        public bool UseHttps { get; set; } = false;

        public string SaveUrl = $"{Directory.GetCurrentDirectory()}\\download\\";
    }

    class Program
    {
        static QiniuOptions Qiniu = new QiniuOptions();
        static void Main(string[] args)
        {
            ListFilesAndDownload();
        }

        /// <summary>
        /// 获取空间文件列表          
        /// </summary>
        static void ListFilesAndDownload()
        {
            Mac mac = new Mac(Qiniu.AK, Qiniu.SK);
            string bucket = Qiniu.Bucket;
            string marker = "";
            string prefix = Qiniu.PrefixPath; // 按文件名前缀保留搜索结果
            string delimiter = ""; // 目录分割字符(比如"/")
            int limit = 100; // 单次列举数量限制(最大值为1000)
            BucketManager bm = new BucketManager(mac, new Config
            {
                Zone = Zone.ZONE_CN_South,
                UseHttps = Qiniu.UseHttps
            });
            int i = 0;
            do
            {
                ListResult result = bm.ListFiles(bucket, prefix, marker, limit, delimiter);
                Console.WriteLine(result);

                marker = result.Result.Marker;
                i++;
                foreach (var item in result.Result.Items)
                {
                    string filename = item.Key;
                    string savepath = Qiniu.SaveUrl + filename.Replace('/', '\\');

                    // 如果文件存在，覆盖则删除，不覆盖则跳过
                    if (File.Exists(savepath))
                    {
                        continue;
                    }

                    // 检查并创建文件夹
                    CheckPath(savepath);

                    string url = Qiniu.Host + filename;
                    var rd = DownloadManager.Download(url, savepath);
                    Console.WriteLine(rd);
                }
            } while (!string.IsNullOrEmpty(marker));

            Console.WriteLine("次:" + i);
        }
        /// <summary>
        /// 检查路径，创建目录
        /// </summary>
        static void CheckPath(string path)
        {
            path = path.Substring(0, path.LastIndexOf('\\'));
            if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
        }

    }
}
