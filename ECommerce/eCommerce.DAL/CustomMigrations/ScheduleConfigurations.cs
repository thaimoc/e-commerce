using System;
using eCommerce.DAL.CustomMigrations.ComplexTypes;
using eCommerce.DAL.Model;

namespace eCommerce.DAL.CustomMigrations
{
    public class ScheduleConfigurations : EntityConfiguration<Schedule, Guid>
    {
        public ScheduleConfigurations()
        {
            /* Declar flowwing codes when DateRange is complextype*/
            Property(s => s.DateRange.Start).HasColumnName("StartDate");
            Property(s => s.DateRange.End).HasColumnName("EndDate");
            /*                   End for complextype                     */
            CreateDefaultInsertUpdateDeleteStoreProcedures(nameof(Schedule));
        }
    }

    
}