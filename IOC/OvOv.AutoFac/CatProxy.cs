using System;
using System.Collections.Generic;
using System.Text;

namespace OvOv.AutoFac
{


    public class CatProxy : ICat
    {
        private readonly ICat cat;
        public CatProxy(ICat cat)
        {
            this.cat = cat;
        }

        public void Eat()
        {
            Console.WriteLine("CatProxy猫在吃东西前");
            cat.Eat();
            Console.WriteLine("CatProxy猫在吃东西后");

        }
    }
}
