using FreeSql;
using OvOv.Core.Domain;

namespace OvOv.FreeSql.AutoFac.DynamicProxy.Repositories
{
    public class TagRepository : DefaultRepository<Tag,int>,ITagRepository
    {
        public TagRepository(UnitOfWorkManager uowm) : base(uowm?.Orm, uowm) 
        {
        }
    }
}
