using FreeSql.DataAnnotations;

namespace OvOv.Core.Domain
{
    public class Tag : IDeleteAduitEntity
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        public string TagName { get; set; }


        public int PostId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
