using System.Data.Entity.ModelConfiguration;
using eCommerce.DAL.Model.ComplexTypes;

namespace eCommerce.DAL.CustomMigrations.ComplexTypes
{
    public class DateRimeRangeConfigurations : ComplexTypeConfiguration<DateTimeRange>
    {
        public DateRimeRangeConfigurations()
        {
            Ignore(x => x.IsDirty);
        }
    }
}