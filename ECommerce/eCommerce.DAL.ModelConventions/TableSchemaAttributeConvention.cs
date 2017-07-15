using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using eCommerce.SharedKernel.Attributes;

namespace eCommerce.DAL.ModelConventions
{
    public class TableSchemaAttributeConvention : Convention
    {
        public TableSchemaAttributeConvention()
        {
            Types()
                .Where(x=>x.GetCustomAttributes(false).OfType<Schema>().Any())
                .Configure(c=>c.ToTable(new EnglishPluralizationService().Pluralize(c.ClrType.Name), c.ClrType.GetCustomAttribute<Schema>().SchemaName));
        }
    }
}