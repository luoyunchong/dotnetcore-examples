using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvOv.AutoFac
{

    public class Cat1 : ICat
    {
        public void Eat()
        {
            Console.WriteLine("猫在吃东西Cat1");
        }
    }

    public class Cat2
    {
        public virtual void Eat()
        {
            Console.WriteLine($"猫在吃东西Cat2");
        }
    }

    /// <summary>
    /// 以特性标签的方式为某个注入
    /// </summary>
    [Intercept(typeof(CatInterceptor))]
    public class Cat3 : ICat
    {
        public void Eat()
        {
            Console.WriteLine("猫在吃东西Cat3");
        }
    }
}
