using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvOv.AutoFac
{
    public class CatInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("CatInterceptor猫吃东西前");
            invocation.Proceed();
            Console.WriteLine("CatInterceptor猫吃东西后");
            Console.WriteLine();

        }
    }

}
