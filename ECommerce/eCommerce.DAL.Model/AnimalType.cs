using System;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL.Model
{
    public class AnimalType : IModificationHistory
    {
        public string Species { get; set; }
        public string Breed { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}