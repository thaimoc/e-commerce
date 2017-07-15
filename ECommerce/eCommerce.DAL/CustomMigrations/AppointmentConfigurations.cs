using eCommerce.DAL.Model;
using System;
using eCommerce.DAL.CustomMigrations.ComplexTypes;

namespace eCommerce.DAL.CustomMigrations
{
    public class AppointmentConfigurations : EntityConfiguration<Appointment, Guid>
    {
        public AppointmentConfigurations()
        {
            HasRequired(a => a.Schedule).WithMany(s=>s.Appointments);
            HasRequired(a => a.Patient);

            Ignore(a => a.Client);

            HasRequired(a => a.Room);
            HasRequired(a => a.AppointmentType);
            HasOptional(a => a.Doctor);
            
            Property(a => a.Title).IsRequired();
            Property(i => i.DateRange.Start).HasColumnName("TimeStart");
            Property(i => i.DateRange.End).HasColumnName("TimeEnd");

            CreateDefaultInsertUpdateDeleteStoreProcedures(nameof(Appointment));
        }
    }
}