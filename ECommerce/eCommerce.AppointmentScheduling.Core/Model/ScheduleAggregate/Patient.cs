using System;
using eCommerce.AppointmentScheduling.Core.Model.ValueObjects;
using eCommerce.SharedKernel;
using eCommerce.SharedKernel.Enums;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.ScheduleAggregate
{
    public class Patient : Entity<int>, IModificationHistory
    {
        public int ClientId { get; private set; }
        public string Name { get; private set; }
        public Gender Gender { get; private set; }
        public AnimalType AnimalType { get; private set; }

        public DateTime DateModified { get; protected set; }
        public DateTime DateCreated { get; protected set; }
        public bool IsDirty { get; protected set; }


        public Patient(int id) : base(id)
        {
            
        }

        private Patient()
        {
            
        }
    }
}