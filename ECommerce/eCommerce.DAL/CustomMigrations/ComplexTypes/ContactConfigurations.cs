using System.Data.Entity.ModelConfiguration;
using eCommerce.DAL.Model.ComplexTypes;

namespace eCommerce.DAL.CustomMigrations.ComplexTypes
{
    public class ContactConfigurations : ComplexTypeConfiguration<Contact>
    {
        public ContactConfigurations()
        {
        }
    }
}