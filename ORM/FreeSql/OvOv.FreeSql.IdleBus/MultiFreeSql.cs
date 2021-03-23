using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FreeSql;
using FreeSql.Internal;

namespace OvOv.FreeSql.IdleBus
{
    public class MultiFreeSql : MultiFreeSql<string> { }

    public partial class MultiFreeSql<TDBKey> : IFreeSql
    {
        internal TDBKey _dbkeyMaster;
        internal AsyncLocal<TDBKey> _dbkeyCurrent = new AsyncLocal<TDBKey>();
        IFreeSql _ormMaster => _ib.Get(_dbkeyMaster);
        IFreeSql _ormCurrent => _ib.Get(object.Equals(_dbkeyCurrent.Value, default(TDBKey)) ? _dbkeyMaster : _dbkeyCurrent.Value);
        internal IdleBus<TDBKey, IFreeSql> _ib;

        public MultiFreeSql()
        {
            _ib = new IdleBus<TDBKey, IFreeSql>();
            _ib.Notice += (_, __) => { };
        }

        public IAdo Ado => _ormCurrent.Ado;
        public IAop Aop => _ormCurrent.Aop;
        public ICodeFirst CodeFirst => _ormCurrent.CodeFirst;
        public IDbFirst DbFirst => _ormCurrent.DbFirst;
        public GlobalFilter GlobalFilter => _ormCurrent.GlobalFilter;
        public void Dispose() => _ib.Dispose();

        public void Transaction(Action handler) => _ormCurrent.Transaction(handler);
        public void Transaction(IsolationLevel isolationLevel, Action handler) => _ormCurrent.Transaction(isolationLevel, handler);

        public ISelect<T1> Select<T1>() where T1 : class => _ormCurrent.Select<T1>();
        public ISelect<T1> Select<T1>(object dywhere) where T1 : class => Select<T1>().WhereDynamic(dywhere);

        public IDelete<T1> Delete<T1>() where T1 : class => _ormCurrent.Delete<T1>();
        public IDelete<T1> Delete<T1>(object dywhere) where T1 : class => Delete<T1>().WhereDynamic(dywhere);

        public IUpdate<T1> Update<T1>() where T1 : class => _ormCurrent.Update<T1>();
        public IUpdate<T1> Update<T1>(object dywhere) where T1 : class => Update<T1>().WhereDynamic(dywhere);

        public IInsert<T1> Insert<T1>() where T1 : class => _ormCurrent.Insert<T1>();
        public IInsert<T1> Insert<T1>(T1 source) where T1 : class => Insert<T1>().AppendData(source);
        public IInsert<T1> Insert<T1>(T1[] source) where T1 : class => Insert<T1>().AppendData(source);
        public IInsert<T1> Insert<T1>(List<T1> source) where T1 : class => Insert<T1>().AppendData(source);
        public IInsert<T1> Insert<T1>(IEnumerable<T1> source) where T1 : class => Insert<T1>().AppendData(source);

        public IInsertOrUpdate<T1> InsertOrUpdate<T1>() where T1 : class => _ormCurrent.InsertOrUpdate<T1>();
    }

    public static class MultiFreeSqlExtensions
    {
        public static IFreeSql TryChange<TDBKey>(this IFreeSql fsql, TDBKey dbkey)
        {
            var multiFsql = fsql as MultiFreeSql<TDBKey>;
            if (multiFsql == null) throw new Exception("fsql 类型不是 MultiFreeSql<TDBKey>");
            if (!multiFsql._dbkeyCurrent.Value.Equals(dbkey))
            {
                multiFsql._dbkeyCurrent.Value = dbkey;
            }
            return multiFsql;
        }

        public static IFreeSql Change<TDBKey>(this IFreeSql fsql, TDBKey dbkey)
        {
            var multiFsql = fsql as MultiFreeSql<TDBKey>;
            if (multiFsql == null) throw new Exception("fsql 类型不是 MultiFreeSql<TDBKey>");
            multiFsql._dbkeyCurrent.Value = dbkey;
            return multiFsql;
        }

        public static IFreeSql Register<TDBKey>(this IFreeSql fsql, TDBKey dbkey, Func<IFreeSql> create)
        {
            var multiFsql = fsql as MultiFreeSql<TDBKey>;
            if (multiFsql == null) throw new Exception("fsql 类型不是 MultiFreeSql<TDBKey>");
            if (multiFsql._ib.TryRegister(dbkey, create))
                if (multiFsql._ib.GetKeys().Length == 1)
                    multiFsql._dbkeyMaster = dbkey;
            return multiFsql;
        }
    }
}
