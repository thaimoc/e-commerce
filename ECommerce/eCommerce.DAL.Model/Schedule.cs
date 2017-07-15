using System;
using System.Collections.Generic;
using eCommerce.DAL.Model.ComplexTypes;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL.Model
{
    public class Schedule : IEntity<Guid>, IModificationHistory
    {
        public Guid Id { get; set; }
        public int ClinicId { get; set; }

        public DateTimeRange DateRange { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

        public Schedule()/* Required for EF */
        {
            Appointments = new List<Appointment>();
        }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}