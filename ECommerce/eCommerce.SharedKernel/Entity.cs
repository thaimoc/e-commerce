using System;
using eCommerce.SharedKernel.Interfaces;

namespace eCommerce.SharedKernel
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        private TId _id;

        public TId Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                if (Equals(value, default(TId)))
                    throw new ArgumentException($"The {nameof(value)} cannot be the type's default value.", nameof(_id));
                _id = value;
            }
        }

        protected Entity(TId id)
        {
            Id = id;
        }
        protected Entity()
        {
            
        }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<TId>;
            return entity != null ? Equals(entity) : base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Entity<TId> other)
        {
            return other != null && Id.Equals(other.Id);
        }
    }
}