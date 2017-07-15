using System.Data.Entity.Migrations.Model;

namespace eCommerce.DAL.ExternalConfigurations.Operations
{
    public class CustomMigrationOperation : MigrationOperation, ICustomMigrationOperation
    {
        /// <summary>
        /// Get Sql Query
        /// </summary>
        public string GetSqlActionFull { get; }

        /// <summary>
        /// Create full sql query action
        /// </summary>
        /// <param name="sqlQueryAction">looks like "CREATE VIEW"/"CREATE STOREPROCEDURE",...</param>
        /// <param name="sqlQueryActionName">looks like "dbo.Customers_Insert",...</param>
        /// <param name="sqlQureyParams">looks like ""/"@CusKey int,"...</param>
        /// <param name="sqlQueryActionBody">looks like body of sql action</param>
        public CustomMigrationOperation(string sqlQueryAction, string sqlQueryActionName, string sqlQureyParams, string sqlQueryActionBody) : base(null)
        {
            GetSqlActionFull =
                $"exec sp_executesql N'{sqlQueryAction} {sqlQueryActionName} {(string.IsNullOrWhiteSpace(sqlQureyParams) ? "" : $"{sqlQureyParams} AS ")} {(sqlQueryAction.ToUpper().Contains("VIEW") ? "AS" : "")} {sqlQueryActionBody} '; ";
        }

        public override bool IsDestructiveChange => false;
    }
}