using System;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class AppointmentType : IModificationHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Duration { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}