using System;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.ApplicationEvents
{
    public class AppointmentScheduledEvent : IApplicationEvent
    {
        public AppointmentScheduledEvent(AppointmentDTO appointment) : this()
        {
            AppointmentScheduled = appointment;
        }

        public AppointmentScheduledEvent()
        {
            DateTimeEventOccurred = DateTime.UtcNow;
        }

        public DateTime DateTimeEventOccurred { get; }
        public AppointmentDTO AppointmentScheduled { get; private set; }
        public string EventType => nameof(AppointmentScheduledEvent);
    }
}