using System;
using eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.Events
{
    public class AppointmentUpdatedEvent : IDomainEvent
    {
        public AppointmentUpdatedEvent(Appointment appointment)
            : this()
        {
            AppointmentUpdated = appointment;
        }
        public AppointmentUpdatedEvent()
        {
            Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccurred { get; }
        public Appointment AppointmentUpdated { get; private set; }
    }
}