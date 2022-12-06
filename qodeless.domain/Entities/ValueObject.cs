using qodeless.domain.Model;
using System;
using System.Collections.Generic;

namespace qodeless.domain.Entities
{
    public interface IUserDataModel
    {
        string UserId { get; set; }
        string RoleId { get; set; }
        string UserName { get; set; }
        string Email { get; set; }
        Guid SiteId { get; set; }
        Guid AccountId { get; set; }
        string Role { get; set; }
        List<ClaimViewModel> Claims { get; set; } 
    }

    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
