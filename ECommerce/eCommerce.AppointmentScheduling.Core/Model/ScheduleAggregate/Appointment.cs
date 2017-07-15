using System;
using System.Runtime.CompilerServices;
using eCommerce.AppointmentScheduling.Core.Model.Events;
using eCommerce.SharedKernel;
using eCommerce.SharedKernel.Enums;
using eCommerce.SharedKernel.Interfaces;

[assembly: InternalsVisibleTo("DDDSessionTests")]
namespace eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Appointment : Entity<Guid>, IModificationHistory
    {
        public Guid ScheduleId { get; private set; }

        public int ClientId { get; private set; }
        
        public int PatientId { get; private set; }

        public int RoomId { get; private set; }

        public int? DoctorId { get; private set; }

        public int AppointmentTypeId { get; private set; }

        public DateTimeRange TimeRange { get; private set; }

        public string Title { get; set; }

        #region More Properties

        public DateTime? DateTimeConfirmed { get; set; }

        //not persisted
        public TrackingState State { get; set; }

        public bool IsPotentiallyConflicting { get; set; }

        #endregion More Properties

        public DateTime DateModified { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public bool IsDirty { get; protected set; }

        public Appointment(Guid id) : base(Guid.NewGuid())
        {

        }

        private Appointment()
        {

        }


        public static Appointment Create(Guid scheduleId, int clientId, int patientId, int roomId, DateTime startTime,
            DateTime endTime, int appointmentTypeId, string title, int doctorId)
        {

            Guard.ForLessEqualZero(new ParameterInfo<int>(clientId, nameof(clientId)));
            Guard.ForLessEqualZero(new ParameterInfo<int>(patientId, nameof(patientId)));
            Guard.ForLessEqualZero(new ParameterInfo<int>(roomId, nameof(roomId)));
            Guard.ForLessEqualZero(new ParameterInfo<int>(appointmentTypeId, nameof(appointmentTypeId)));
            Guard.ForNullOrEmpty(new ParameterInfo<string>(title, nameof(title)));


            var appointment = new Appointment(Guid.NewGuid())
            {
                ScheduleId = scheduleId,
                PatientId = patientId,
                ClientId = clientId,
                RoomId = roomId,
                TimeRange = new DateTimeRange(startTime, endTime),
                AppointmentTypeId = appointmentTypeId,
                DoctorId = doctorId,
                Title = title
            };

            return appointment;
        }

        public void UpdateRoom(int roomId)
        {
            if (roomId == RoomId) return;

            RoomId = roomId;

            var appoinmentUpdateEvent = new AppointmentScheduledEvent(this);
            DomainEvents.Raise(appoinmentUpdateEvent);
        }

        public void UpdateTime(DateTimeRange newStartEnd)
        {
            if (newStartEnd == TimeRange) return;

            TimeRange = newStartEnd;

            var appointmentUpdatedEvent = new AppointmentUpdatedEvent(this);
            DomainEvents.Raise(appointmentUpdatedEvent);
        }

        public void Confirm(DateTime dateConfirmed)
        {
            if (DateTimeConfirmed.HasValue) return; // no need to reconfirm

            DateTimeConfirmed = dateConfirmed;

            var appointmentConfirmedEvent = new AppointmentConfirmedEvent(this);
            DomainEvents.Raise(appointmentConfirmedEvent);
        }
    }
}