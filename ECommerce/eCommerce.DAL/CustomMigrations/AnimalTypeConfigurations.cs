using eCommerce.DAL.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace eCommerce.DAL.CustomMigrations
{
    public class AnimalTypeConfigurations : EntityTypeConfiguration<AnimalType>
    {
        public AnimalTypeConfigurations()
        {
            Ignore(x => x.IsDirty);

            HasKey(x => new { x.Species, x.Breed });
            Property(x => x.Species)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AnimalTypeSpecies")
                {
                    IsClustered = false,
                    IsUnique = false,
                    Order = 0
                }));

            Property(x => x.Breed)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("AnimalTypeBreed")
                {
                    IsClustered = false,
                    IsUnique = false,
                    Order = 1
                }));

            MapToStoredProcedures(t =>
            {
                t.Insert(config => config.HasName($"{nameof(AnimalType)}s_Insert"));
                t.Update(config => config.HasName($"{nameof(AnimalType)}s_Update"));
                t.Delete(config => config.HasName($"{nameof(AnimalType)}s_Delete"));
            });
        }
    }
}