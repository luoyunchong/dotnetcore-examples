namespace OvOv.Core.Domain
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
