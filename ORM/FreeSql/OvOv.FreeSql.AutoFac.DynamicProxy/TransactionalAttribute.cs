using System;
using System.Data;
using FreeSql;

namespace OvOv.FreeSql.AutoFac.DynamicProxy
{
    /// <summary>
    /// 标志事务的特性标签
    /// </summary>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class TransactionalAttribute : Attribute
    {
        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
    }
}