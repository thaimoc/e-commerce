using System.Data.Entity.Migrations.Model;

namespace eCommerce.DAL.ExternalConfigurations.Operations
{
    public class CreateDataContextOperation : DatabaseCollationOperation, ICustomMigrationOperation
    {
        public bool ChangeTracking { get; }

        public CreateDataContextOperation(string databaseName, string collation, bool changeTracking) : base(databaseName, collation)
        {
            ChangeTracking = changeTracking;
            GetSqlActionFull = $"exec sp_executesql N' ALTER DATABASE [{databaseName}] SET CHANGE_TRACKING = {(ChangeTracking ? "ON" : "OFF")}  '";
        }

        public override bool IsDestructiveChange => false;
        public string GetSqlActionFull { get; }
    }
}