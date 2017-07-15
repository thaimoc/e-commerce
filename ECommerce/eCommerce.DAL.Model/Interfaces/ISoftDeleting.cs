namespace eCommerce.DAL.Model.Interfaces
{
    public interface ISoftDeleting
    {
        bool IsDeleted { get; set; }
    }
}