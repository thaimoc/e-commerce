using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using eCommerce.DAL.CustomMigrations.ComplexTypes;
using eCommerce.DAL.Model;

namespace eCommerce.DAL.CustomMigrations
{
    public class PatientConfigurations : EntityConfiguration<Patient, int>
    {
        public PatientConfigurations()
        {

            HasRequired(n => n.Owner);
            HasRequired(p => p.AnimalType);

            Property(x => x.Name).IsRequired()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("PatientName")
                {
                    IsClustered = false,
                    IsUnique = false,
                    Order = 1
                }));

            CreateDefaultInsertUpdateDeleteStoreProcedures(nameof(Patient));
        }
    }
}