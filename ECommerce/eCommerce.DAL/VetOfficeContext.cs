using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.DAL.ModelConventions;
using eCommerce.DAL.Model;
using eCommerce.DAL.Model.Attributes;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL
{
    public class VetOfficeContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }


        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Types().Where(t=>t is IModificationHistory).Configure(c=>c.Ignore("IsDirty"));

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Properties<Guid>().Where(p => p.Name == "Id").Configure(c => c.IsKey());
            modelBuilder.Properties<int>().Where(p => p.Name == "Id").Configure(c => c.IsKey());

            const string defaulSchema = "VetOffice_dbo";
            var clientSchema = defaulSchema;
            var intanceOfClientSchema = typeof(Client).GetCustomAttributes(typeof(Schema)).FirstOrDefault() as Schema;
            if (intanceOfClientSchema != null)
            {
                clientSchema = intanceOfClientSchema.SchemaName;
            }

            //modelBuilder.Entity<Client>().Property(c => c.FullName).HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute($"Idx_{clientSchema}.{nameof(Client)}_FullName", 2)));

            modelBuilder.HasDefaultSchema(clientSchema);
            modelBuilder.Conventions.AddFromAssembly(typeof(ModelConventionsContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {

            Func<int> callback = base.SaveChanges;

            return SaveChangeCustom(callback);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            Func<Task<int>> callbackAsync = () => base.SaveChangesAsync(cancellationToken);

            return SaveChangeCustom(callbackAsync);
        }

        private TOut SaveChangeCustom<TOut>(Func<TOut> callback)
        {
            foreach (var history in ChangeTracker.Entries()
                .Where(e => e.Entity is IModificationHistory && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .Select(e => e.Entity as IModificationHistory))
            {
                history.DateModified = DateTime.Now;
                if (history.DateCreated == DateTime.MinValue)
                {
                    history.DateCreated = DateTime.Now;
                }
            }

            var result = Task.Run(callback).Result;

            foreach (
                var history in
                    ChangeTracker.Entries()
                        .Where(e => e.Entity is IModificationHistory)
                        .Select(e => e.Entity as IModificationHistory))
            {
                history.IsDirty = false;
            }

            return result;
        }
    }
}