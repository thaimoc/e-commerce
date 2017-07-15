using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using eCommerce.DAL.ExternalConfigurations.Interceptor;

namespace eCommerce.DAL.ExternalConfigurations
{
    public class CustomDbConfiguration : DbConfiguration
    {
        public CustomDbConfiguration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory("Data Source=DESKTOP-RI4P4PV;Integrated Security=True;MultipleActiveResultSets=True"));
            SetHistoryContext(SqlProviderServices.ProviderInvariantName, (connection, defaultShema) => new MigrationsHistoryTableContext(connection, defaultShema));
            
            SetMigrationSqlGenerator(SqlProviderServices.ProviderInvariantName, () => new CustomSqlMigrationSqlGenerator());

            AddInterceptor(new LoggingInterceptor());
        }
    }
}