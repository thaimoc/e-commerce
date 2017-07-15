using System;
using eCommerce.SharedKernel;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Room : Entity<int>, IModificationHistory
    {
        public virtual  string Name { get; set; }
        public DateTime DateModified { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public bool IsDirty { get; protected set; }
    }
}