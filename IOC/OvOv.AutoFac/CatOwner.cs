using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace OvOv.AutoFac
{
    /// <summary>
    /// 如何给这个“铲屎官”类  动态的为这个代理类添加一个接口
    /// </summary>
    public class CatOwner
    {

    }





    public class CatOwnerInterceptor : IInterceptor
    {
        private readonly ICat cat;
        public CatOwnerInterceptor(ICat cat)
        {
            this.cat = cat;
        }

        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("通过AOP给类增加方法");

            invocation.Method.Invoke(cat, invocation.Arguments);
        }
    }
}
