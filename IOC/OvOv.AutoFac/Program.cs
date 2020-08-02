using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;

namespace OvOv.AutoFac
{
    /// <summary>
    /// https://www.cnblogs.com/stulzq/p/8547839.html Autofac高级用法之动态代理
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            RunCatProxy();

            ContainerBuilder builder = new ContainerBuilder();
            //先将拦截器注册到容器中。
            builder.RegisterType<CatInterceptor>();
            builder.RegisterType<CatOwnerInterceptor>();

            //将ICat、Cat注册到容器中，并增加拦截器，CatInterceptor，以接口形式设置拦截, 三种方式作用相同
            builder.RegisterType<Cat1>().As<ICat>().Named<ICat>("cat1").InterceptedBy(typeof(CatInterceptor)).EnableInterfaceInterceptors();
            builder.RegisterType<Cat2>().InterceptedBy(typeof(CatInterceptor)).EnableClassInterceptors();
            builder.RegisterType<Cat3>().As<ICat>().Named<ICat>("cat3").EnableInterfaceInterceptors();


            ///给铲屎官这个类，增加一个拦截器，并启用类拦截器，并注册ICat接口
            builder.RegisterType<CatOwner>().InterceptedBy(typeof(CatOwnerInterceptor)).EnableClassInterceptors(ProxyGenerationOptions.Default, additionalInterfaces: typeof(ICat));

            var container = builder.Build();


            ICat cat1 = container.ResolveNamed<ICat>("cat1");
            cat1.Eat();

            Cat2 cat2 = container.Resolve<Cat2>();
            cat2.Eat();

            ICat cat3 = container.ResolveNamed<ICat>("cat3");
            cat3.Eat();


            CatOwner catOwner = container.Resolve<CatOwner>();

            ///通过反射获取代理类CatOwner中的Eat方法，然后执行Eat方法， 但这个是会报错的，NotImplementedException
            catOwner.GetType().GetMethod("Eat").Invoke(catOwner, null);





            Console.ReadKey();
        }

        /// <summary>
        /// 静态代理实现AOP
        /// </summary>
        static void RunCatProxy()
        {
            ICat icat = new Cat1();

            var catProxy = new CatProxy(icat);

            catProxy.Eat();

            Console.WriteLine();
        }

        /// <summary>
        /// 没有AOP，
        /// </summary>
        static void RunCat()
        {
            ICat icat = new Cat1();

            icat.Eat();
        }
    }
}
