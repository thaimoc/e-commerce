using System;
using eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.Events
{
    public class AppointmentScheduledEvent : IDomainEvent
    {
        public AppointmentScheduledEvent(Appointment appointment) : this()
        {
            AppointmentScheduled = appointment;
        }

        public AppointmentScheduledEvent()
        {
            Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public DateTime DateTimeEventOccurred { get; }

        public Appointment AppointmentScheduled { get; private set; }
    }
}