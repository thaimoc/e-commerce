using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using eCommerce.AppointmentScheduling.Core.Model.Events;
using eCommerce.SharedKernel;
using eCommerce.SharedKernel.Enums;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Schedule : Entity<Guid>, IModificationHistory
    {
        public int ClinicId { get; private set; }

        public DateTimeRange DateRange { get; private set; }

        protected virtual ICollection<Appointment> _appointments { get; set; }

        public IEnumerable<Appointment> Appointments
        {
            get { return _appointments; } /* AssReadOnly can't be cast to a list*/
            private set { _appointments = (List<Appointment>)value; }
        }

        private Schedule() : base(Guid.NewGuid())
        {
            _appointments = new List<Appointment>();
        }

        public Schedule(Guid id, DateTimeRange dateRange, int clinicId, IEnumerable<Appointment> appointments)
            : base(Guid.NewGuid())
        {
            ClinicId = clinicId;
            DateRange = dateRange;
            _appointments = new List<Appointment>(appointments);
            DateRange = new DateTimeRange(DateTime.UtcNow, new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + 7));
            MarkConflictingAppointments();

            DomainEvents.Register<AppointmentUpdatedEvent>(Handle);

        }

        public Appointment AddNewAppointment(Appointment appointment)
        {
            if (_appointments.Any(a=>a.Id == appointment.Id))
            {
                throw new ArgumentException("Cannot add duplicate appointment to schedule.", nameof(appointment));
            }

            appointment.State = TrackingState.Added;
            _appointments.Add(appointment);

            MarkConflictingAppointments();

            var appointmentScheduleEvent = new AppointmentScheduledEvent(appointment);
            DomainEvents.Raise(appointmentScheduleEvent);

            return appointment;
        }

        public void DeleteAppointment(Appointment appointment)
        {
            // mark the appointment for deletion by the repository
            var appointmentToDelete = Appointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (appointmentToDelete != null)
            {
                appointmentToDelete.State = TrackingState.Deleted;
            }

            MarkConflictingAppointments();
        }

        private void Handle(AppointmentUpdatedEvent args)
        {
            MarkConflictingAppointments();
        }

        private void MarkConflictingAppointments()
        {
            foreach (var appointment in Appointments)
            {
                var potentiallyConflictingAppoinments = Appointments
                    .Where(a => a.PatientId == appointment.PatientId &&
                                a.TimeRange.Overlaps(appointment.TimeRange) &&
                                a.Id != appointment.Id &&
                                a.State != TrackingState.Deleted).ToList();

                potentiallyConflictingAppoinments.ForEach(a => a.IsPotentiallyConflicting = true);

                appointment.IsPotentiallyConflicting = potentiallyConflictingAppoinments.Any();
            }
        }

        public DateTime DateModified { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public bool IsDirty { get; protected set; }

        public class ScheduleConfigurations : EntityTypeConfiguration<Schedule>
        {
            public ScheduleConfigurations()
            {
                Ignore(s => s.IsDirty);

                /* Declar flowwing codes when DateRange is complextype*/
                Property(s => s.DateRange.Start).HasColumnName("StartDate");
                Property(s => s.DateRange.End).HasColumnName("EndDate");
                /*                   End for complextype                     */

                HasMany(x => x._appointments);
            }
        }

    }
}