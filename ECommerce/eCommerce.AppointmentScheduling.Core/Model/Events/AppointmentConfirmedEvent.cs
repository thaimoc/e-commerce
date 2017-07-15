using System;
using eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.Events
{
    public class AppointmentConfirmedEvent : IDomainEvent
    {
        public AppointmentConfirmedEvent(Appointment appointment) : this()
        {
            AppointmentUpdated = appointment;
        }

        public AppointmentConfirmedEvent()
        {
            Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public DateTime DateTimeEventOccurred { get; private set; }

        public Appointment AppointmentUpdated { get; private set; }
    }
}