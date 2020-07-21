using FreeSql;
using OvOv.Core.Domain;

namespace OvOv.FreeSql.Repository.Repositories
{
    public class TagRepository : DefaultRepository<Tag,int>,ITagRepository
    {
        public TagRepository(UnitOfWorkManager uowm) : base(uowm?.Orm, uowm) 
        {
        }
    }
}
