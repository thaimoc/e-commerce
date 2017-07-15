namespace eCommerce.SharedKernel.Interfaces
{
    public interface ISoftDeleting
    {
        bool IsDeleted { get; set; }
    }
}