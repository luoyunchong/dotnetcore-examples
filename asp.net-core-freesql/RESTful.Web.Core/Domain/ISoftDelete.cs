namespace RESTful.Web.Core.Domain
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
