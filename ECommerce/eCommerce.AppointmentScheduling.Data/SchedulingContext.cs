using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate;
using eCommerce.AppointmentScheduling.Core.Model.ValueObjects;
using eCommerce.SharedKernel;
using eCommerce.SharedKernel.Attributes;

namespace eCommerce.AppointmentScheduling.Data
{
    public class SchedulingContext : DbContext
    {

        public DbSet<Schedule> Schedules { get; set; }

        //public DbSet<Schedule> Schedules
        //{
        //    get
        //    {
        //        _schedules.Include("_appointments"); // eager loading with Include
        //        return _schedules;
        //    }
        //    set { _schedules = value; }
        //}

        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }

        public SchedulingContext() : base("name=eCommerce.DAL.VetOfficeContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Configurations.Add(new Schedule.ScheduleConfigurations());

            const string defaulSchema = "VetOffice_dbo";
            var clientSchema = defaulSchema;
            var intanceOfClientSchema = typeof(Client).GetCustomAttributes(typeof(Schema)).FirstOrDefault() as Schema;
            if (intanceOfClientSchema != null)
            {
                clientSchema = intanceOfClientSchema.SchemaName;
            }

            modelBuilder.HasDefaultSchema(clientSchema);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class ContactConfigurations : ComplexTypeConfiguration<Contact>
    {
    }

    public class DateRimeRangeConfigurations : ComplexTypeConfiguration<DateTimeRange>
    {
        public DateRimeRangeConfigurations()
        {
            Ignore(x => x.IsDirty);
        }
    }

    public class ClientConfigurations : EntityTypeConfiguration<Client>
    {
        public ClientConfigurations()
        {
            Ignore(c => c.IsDirty);
            Property(c => c.Contact.FullName.FirstName).IsRequired().HasColumnName("FirstName");
            Property(c => c.Contact.FullName.LastName).IsRequired().HasColumnName("LastName");
            Property(c => c.Contact.CompanyName).IsRequired().HasColumnName("CompanyName");
            Property(c => c.Contact.EmailAddress).IsRequired().HasColumnName("Email");
            Property(c => c.Contact.Phone).IsRequired().HasColumnName("PhoneNumbers");
        }
    }

    public class DoctorConfigurations : EntityTypeConfiguration<Doctor>
    {
        public DoctorConfigurations()
        {
            Ignore(c => c.IsDirty);
        }
    }

    public class AnimalTypeConfigurations : EntityTypeConfiguration<AnimalType>
    {
        public AnimalTypeConfigurations()
        {
            Ignore(t => t.IsDirty);
            HasKey(x => new { x.Species, x.Breed });
        }
    }

    public class PatientConfigurations : EntityTypeConfiguration<Patient>
    {
        public PatientConfigurations()
        {
            Ignore(p => p.IsDirty);
            Property(p => p.ClientId).IsRequired().HasColumnName("OwnerFKId");
            HasRequired(p => p.AnimalType);
        }
    }

    

    public class RoomConfigurations : EntityTypeConfiguration<Room>
    {
        public RoomConfigurations()
        {
            Ignore(c => c.IsDirty);
        }
    }

    public class AppointmentConfigurations : EntityTypeConfiguration<Appointment>
    {
        public AppointmentConfigurations()
        {
            Ignore(a => a.IsDirty);
            Property(i => i.TimeRange.Start).HasColumnName("TimeStart");
            Property(i => i.TimeRange.End).HasColumnName("TimeEnd");

            Property(i => i.AppointmentTypeId).HasColumnName("AppointmentTypeFKId");
            Property(i => i.DoctorId).HasColumnName("DoctorFKId");
            Property(i => i.PatientId).HasColumnName("PatientFKId");
            Property(i => i.RoomId).HasColumnName("RoomFKId");
            Property(i => i.ScheduleId).HasColumnName("ScheduleFKId");

            Ignore(a => a.ClientId);
        }
    }

    public class AppointmentTypeConfigurations : EntityTypeConfiguration<AppointmentType>
    {
        public AppointmentTypeConfigurations()
        {
            Ignore(a => a.IsDirty);
        }
    }

    //public class ScheduleConfigurations : EntityTypeConfiguration<Schedule>
    //{
    //    public ScheduleConfigurations()
    //    {
    //        Ignore(s => s.IsDirty);

    //        /* Declar flowwing codes when DateRange is complextype*/
    //        Property(s => s.DateRange.Start).HasColumnName("StartDate");
    //        Property(s => s.DateRange.End).HasColumnName("EndDate");
    //        /*                   End for complextype                     */
    //    }
    //}
}