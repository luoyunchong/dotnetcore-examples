using System.Data;
using DotNetCore.CAP;
using FreeSql;
using Microsoft.Extensions.DependencyInjection;

namespace Cap.FreeSql
{
    public static class FreeSqlDbConnectExtensions
    {
        /// <summary>
        /// 用的是二个不同的dbcontenxt
        /// </summary>
        /// <param name="freeSql"></param>
        /// <param name="publisher"></param>
        /// <param name="autoCommit"></param>
        /// <returns></returns>
        public static ICapTransaction BeginTransaction(this IFreeSql freeSql, ICapPublisher publisher, bool autoCommit = false)
        {
            var dbTrans1 = freeSql.CreateUnitOfWork().GetOrBeginTransaction();
            var dbTrans2 = freeSql.CreateDbContext().UnitOfWork.GetOrBeginTransaction();

            publisher.Transaction.Value = publisher.ServiceProvider.GetService<ICapTransaction>();
            return publisher.Transaction.Value.Begin(dbTrans1, autoCommit);

        }

        /// <summary>
        /// 这个就一直报错。UnitOfWork用的是同一个dbContext
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="publisher"></param>
        /// <param name="autoCommit"></param>
        /// <returns></returns>
        public static ICapTransaction BeginTransaction(this IRepositoryUnitOfWork unitOfWork, ICapPublisher publisher, bool autoCommit = false)
        {
            var dbTransaction = unitOfWork.GetOrBeginTransaction();

            publisher.Transaction.Value = publisher.ServiceProvider.GetService<ICapTransaction>();
            return publisher.Transaction.Value.Begin(dbTransaction, autoCommit);

        }
    }
}
