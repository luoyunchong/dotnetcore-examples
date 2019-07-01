using System;

namespace CsRedis.Console_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var csredis = new CSRedis.CSRedisClient("127.0.0.1:6379,password=,defaultDatabase=CsRedis,prefix=CsRedis_ConSole_Example");
            RedisHelper.Initialization(csredis);

            RedisHelper.Set("test1", "123123", 60);
            string result = RedisHelper.Get("test1");
            Console.WriteLine("key:test1,value:" + result);

            Console.ReadKey();
        }
    }
}
