using System;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL.Model
{
    public class AppointmentType : IEntity<int>, IModificationHistory
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