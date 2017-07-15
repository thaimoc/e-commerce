using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using eCommerce.DAL.CustomMigrations.ComplexTypes;
using eCommerce.DAL.Model;

namespace eCommerce.DAL.CustomMigrations
{
    public class RoomConfigurations : EntityConfiguration<Room, int>
    {
        public RoomConfigurations()
        {
            Property(x => x.Name).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("RoomName")
                {
                    IsClustered = false,
                    IsUnique = false,
                    Order = 1
                }));
            CreateDefaultInsertUpdateDeleteStoreProcedures(nameof(Room));
        }
    }
}