using System;
using CSRedis;

namespace OvOv.CsRedis.ConsoleDemo
{
    class Program
    {
        static void Main()
        {
            CSRedisClient csredis = new CSRedisClient("127.0.0.1:6379,password=,defaultDatabase=CsRedis,prefix=CsRedis_");
            RedisHelper.Initialization(csredis);

            Test();
            Console.ReadKey();
        }

        static void Test()
        {
            //1.set key value [ex seconds] [px milliseconds] [nx|xx]
            //setex key seconds value #设定键的值，并指定此键值对应的 有效时间。
            //setnx key value  #键必须 不存在，才可以设置成功。如果键已经存在，返回 0。
            RedisHelper.Set("redis-key", "just a string value", 50);//setex "redis-key" 50 "just a string value"

            RedisHelper.Set("redis-key-class", DateTime.Now, 30);

            //1.1.2. 获取值
            //get key
            //如果要获取的 键不存在，则返回 nil（空）。
            string redisValue = RedisHelper.Get("redis-key");
            Console.WriteLine($"setex redis-key 50 just a string value ,RedisHelper.Get()得到值如下：{redisValue}");
            DateTime now = RedisHelper.Get<DateTime>("redis-key-class");
            Console.WriteLine($"setex redis-key-class DateTime.Now,RedisHelper.Get()值如下{now}");

            //1.1.3. 批量设置值
            //mset key value [key value ...]
            RedisHelper.MSet("a", "1", "b", "2", "c", "3", "d", "4");//等价于mset a 1 b 2 c 3 d 4


            //1.1.4. 批量获取值
            //mget key [key ...]

            string[] mgetValues = RedisHelper.MGet<string>("a", "b", "c", "d");
            Console.WriteLine($"mset a 1 b 2 c 3 d 4, RedisHelper.MGet()得到的值是");
            foreach (var mgetValue in mgetValues)
            {
                Console.Write($"{mgetValue}、");
            }
            Console.WriteLine();

            //1.1.5. 计数
            //incr key
            //incr 命令用于对值做 自增操作

            //自增指定数字
            long incr = RedisHelper.IncrBy("key");
            Console.WriteLine($"incr key, incr得到的值是{incr}");
            //设置自增数字的增量值
            incr = RedisHelper.IncrBy("key", 2);
            Console.WriteLine($"再次incrby key 2, incr得到的值是{incr}");

            incr = RedisHelper.IncrBy("key", -2);
            Console.WriteLine($"再次decrby key -2, incr得到的值是{incr}");

            //exists key
            bool isExistsKey = RedisHelper.Exists("new-key");
            Console.WriteLine($"exists key ,value：{isExistsKey}");

            decimal incrByFloat = RedisHelper.IncrByFloat("key-float", (decimal)0.1F);
            Console.WriteLine($"incrbyfloat key-float 0.1,value：{incrByFloat}");

        }
    }
}
