namespace eCommerce.DAL.ExternalConfigurations.Operations
{
    interface ICustomMigrationOperation
    {
        string GetSqlActionFull { get; }
    }
}