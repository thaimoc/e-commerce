using System;
using eCommerce.DAL.Model.Interfaces;

namespace eCommerce.DAL.Model
{
    public class Room : IEntity<int>, ISoftDeleting, IModificationHistory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
        public bool IsDeleted { get; set; }
    }
}