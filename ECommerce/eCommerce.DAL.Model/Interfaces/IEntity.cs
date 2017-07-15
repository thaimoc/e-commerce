namespace eCommerce.DAL.Model.Interfaces
{
    public interface IEntity<TId> where TId : struct
    {
        TId Id { get; set; }
    }
}