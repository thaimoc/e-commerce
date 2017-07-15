using System;
using System.Data.Entity.ModelConfiguration;
using System.Threading.Tasks;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL.CustomMigrations.ComplexTypes
{
    public class EntityConfiguration<TEnitytype, TId> : EntityTypeConfiguration<TEnitytype> where TEnitytype : class, IModificationHistory, IEntity<TId> where TId : struct
    {
        public EntityConfiguration()
        {
            Ignore(x => x.IsDirty);
        }
        protected void CreateDefaultInsertUpdateDeleteStoreProcedures(string tableName)
        {
            Action funcToCreateStoreProcedures = () =>
            {
                MapToStoredProcedures(s =>
                {
                    s.Insert(i =>
                    {
                        i.HasName($"[{tableName}s_Insert]");
                    });
                    s.Update(u =>
                    {
                        u.HasName($"[{tableName}s_Update]");
                    });
                    s.Delete(d =>
                    {
                        d.HasName($"[{tableName}s_Delete]").Parameter(c => c.Id, $"{tableName}Id");
                    });
                });
            };

            Task.Run(funcToCreateStoreProcedures);
        }
    }
}