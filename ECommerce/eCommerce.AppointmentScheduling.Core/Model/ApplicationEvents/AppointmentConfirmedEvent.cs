using System;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.ApplicationEvents
{
    public class AppointmentConfirmedEvent : IApplicationEvent, IModificationHistory
    {
        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccurred { get; set; }
        public Guid AppointmentId { get; set; }

        public string EventType => nameof(AppointmentConfirmedEvent);

        public AppointmentConfirmedEvent()
        {
            Id = Guid.NewGuid();
            DateTimeEventOccurred = DateTime.UtcNow;
        }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}