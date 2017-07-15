using System;
using eCommerce.DAL.Model.ComplexTypes;
using eCommerce.DAL.Model.Enums;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL.Model
{
    public class Appointment : IEntity<Guid>, IModificationHistory
    {
        public Guid Id { get; set; }
        public Schedule Schedule { get; set; }

        public Client Client { get; set; }

        public Patient Patient { get; set; }

        public Room Room { get; set; }

        public Doctor Doctor { get; set; }

        public AppointmentType AppointmentType { get; set; }

        public DateTimeRange DateRange { get; set; }

        public string Title { get; set; }

        #region More Properties

        public DateTime? DateTimeConfirmed { get; set; }

        //not persisted
        public TrackingState State { get; set; }

        public bool IsPotentiallyConflicting { get; set; }

        #endregion More Properties

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}