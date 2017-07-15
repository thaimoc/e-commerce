using System;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.SqlServer;
using eCommerce.DAL.ExternalConfigurations.Operations;

namespace eCommerce.DAL.ExternalConfigurations
{
    public class CustomSqlMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {

        protected override void Generate(AlterTableOperation alterTableOperation)
        {
            Action<AlterTableOperation, string, Action<AnnotationValues, Action<AnnotationValues, IndentedTextWriter>>, Action<AnnotationValues, IndentedTextWriter>> func = (alter, annotationName, funcwriter, act) =>
            {
                AnnotationValues annoValues;
                if (alterTableOperation.Annotations.TryGetValue(annotationName, out annoValues))
                    funcwriter(annoValues, act);
            };
            Action<AnnotationValues, Action<AnnotationValues, IndentedTextWriter>> funcIndentedTextWriter = (annoValues, action) =>
            {
                using (IndentedTextWriter writer = Writer())
                {
                    action(annoValues, writer);
                    Statement(writer);
                }
            };
            Action<AnnotationValues, IndentedTextWriter> funcChangeTracking = (anoVals, wr) =>
            {
                string[] settingOpts = { "ENABLE", "DISABLE", "True" };
                var setting = anoVals.NewValue != null ? (anoVals.NewValue.ToString() == settingOpts[2] ? settingOpts[0] : settingOpts[1]) : settingOpts[1];
                wr.WriteLine($"exec sp_executesql N'ALTER TABLE [{alterTableOperation.Name}] {setting} CHANGE_TRACKING';");
            };
            Func<AnnotationValues, string[]> funcNeedSplit = anoVals => anoVals.NewValue.ToString().Split('|');
            Action<AnnotationValues, IndentedTextWriter> funcView = (anoVals, wr) =>
            {
                string[] newValues = funcNeedSplit(anoVals);
                wr.WriteLine($"exec sp_executesql N'CREATE VIEW {newValues[0]} AS {newValues[1]}'; ");
            };
            Action<AnnotationValues, IndentedTextWriter> funcStorePorcedures = (anoVals, wr) =>
            {
                string[] newValues = funcNeedSplit(anoVals);
                for (int i = 1; i < newValues.Length; i += 2)
                {
                    wr.WriteLine($"exec sp_executesql N'CREATE PROCEDURE {newValues[i - 1]} {newValues[i]}'; ");
                }
            };

            func(alterTableOperation, "TrackDatabaseChanges", funcIndentedTextWriter, funcChangeTracking);
            func(alterTableOperation, "View", funcIndentedTextWriter, funcView);
            func(alterTableOperation, "StoredProcedures", funcIndentedTextWriter, funcStorePorcedures);
        }

        protected override void Generate(MigrationOperation migrationOperation)
        {
            Action<MigrationOperation, Action<string>> func = (mig, act) =>
            {
                var orgMig = mig as ICustomMigrationOperation;
                if (orgMig != null)
                {
                    act(orgMig.GetSqlActionFull);
                }
            };
            Action<string> funcExecAction = (sqlQuery) =>
            {

                using (IndentedTextWriter writer = Writer())
                {
                    writer.WriteLine(sqlQuery);

                    Statement(writer);
                }
            };

            func(migrationOperation, funcExecAction);
        }

    }
}