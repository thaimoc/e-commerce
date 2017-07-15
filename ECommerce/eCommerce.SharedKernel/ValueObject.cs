using System;
using System.Collections.Generic;
using System.Reflection;

namespace eCommerce.SharedKernel
{
    //this base class comes from Jimmy Bogard
    //http://grabbagoft.blogspot.com/2007/06/generic-value-object-equality.html
    //note that this implementation does not take Collections into account
    public abstract class ValueObject<T> : IEquatable<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            T other = obj as T;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            IEnumerable<FieldInfo> fields = GetFields();

            const int startValue = 17;
            var multiplier = 59;

            var hashCode = startValue;

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(this);

                if (value != null)
                    hashCode = hashCode*multiplier + value.GetHashCode();
            }

            return hashCode;
        }

        public virtual bool Equals(T other)
        {
            if (other == null)
            {
                return false;
            }

            Type t = GetType();
            Type otherType = other.GetType();

            if (t != otherType) return false;

            FieldInfo[] fields = t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                object otherValue = field.GetValue(other);
                object thisValue = field.GetValue(this);

                if (otherValue == null)
                {
                    if (thisValue != null)
                    {
                        return false;
                    }
                }
                else if (!otherValue.Equals(thisValue))
                {
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            var t = GetType();
            var fields = new List<FieldInfo>();

            while (t != typeof(object) && t != null)
            {
                fields.AddRange(t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));
                t = t.BaseType;
            }

            return fields;
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }
    }
}