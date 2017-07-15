using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using eCommerce.DAL.CustomMigrations.ComplexTypes;
using eCommerce.DAL.Model;

namespace eCommerce.DAL.CustomMigrations
{
    public class DoctorConfigurations : EntityConfiguration<Doctor, int>
    {
        public DoctorConfigurations()
        {
            Property(d => d.Name).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("DoctorName")
            {
                IsUnique = false,
                IsClustered = false,
                Order = 3
            }));

            CreateDefaultInsertUpdateDeleteStoreProcedures(nameof(Doctor));
        }
    }
}