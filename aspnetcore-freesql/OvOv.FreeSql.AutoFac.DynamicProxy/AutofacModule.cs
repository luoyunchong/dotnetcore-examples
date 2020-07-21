using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using OvOv.FreeSql.AutoFac.DynamicProxy.Services;

namespace OvOv.FreeSql.AutoFac.DynamicProxy
{
   
    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkInterceptor>();
            builder.RegisterType<UnitOfWorkAsyncInterceptor>();
            
            builder.RegisterType<BlogService>()
                .InterceptedBy( typeof(UnitOfWorkInterceptor))
                .EnableClassInterceptors();
                
            builder.RegisterType<TagService>().AsSelf()
                .InstancePerLifetimeScope()
                .InterceptedBy( typeof(UnitOfWorkInterceptor))
                .EnableClassInterceptors();
        }
    }
}