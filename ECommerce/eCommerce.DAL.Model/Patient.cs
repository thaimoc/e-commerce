using System;
using eCommerce.DAL.Model.Enums;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL.Model
{
    public class Patient : IEntity<int>, IModificationHistory
    {
        public int Id { get; set; }
        public Client Owner { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public AnimalType AnimalType { get; set; }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}