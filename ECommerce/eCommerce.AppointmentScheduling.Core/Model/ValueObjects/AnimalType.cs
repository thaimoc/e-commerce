using System;
using eCommerce.SharedKernel;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.AppointmentScheduling.Core.Model.ValueObjects
{
    public class AnimalType : ValueObject<AnimalType>, IModificationHistory
    {
        public string Species { get; private set; }
        public string Breed { get; private set; }

        public AnimalType()
        {
            
        }

        public AnimalType(string species, string breed)
        {
            Species = species;
            Breed = breed;
        }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}