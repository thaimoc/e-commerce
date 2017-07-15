using eCommerce.DAL.CustomMigrations.ComplexTypes;
using eCommerce.DAL.Model;

namespace eCommerce.DAL.CustomMigrations
{
    public class AppointmentTypeConfigurations : EntityConfiguration<AppointmentType, int>
    {
        public AppointmentTypeConfigurations()
        {
            Property(t => t.Code).IsRequired().HasMaxLength(128);
            CreateDefaultInsertUpdateDeleteStoreProcedures(nameof(AppointmentType));
        }
    }
}