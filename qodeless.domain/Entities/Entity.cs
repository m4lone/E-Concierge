using System;

namespace qodeless.domain.Entities
{
    public class Entity
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public bool Excluded { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid(); //aqui que nasce o ID de banco
            var now = DateTime.Now;
            CreatedAt = now;
            UpdatedAt = now;
            DeletedAt = new DateTime(1970, 01, 01);
            Excluded = false;
        }

        public void Delete()
        {
            Excluded = true;
            DeletedAt = DateTime.Now; ;
        }

        public void Update()
        {
            UpdatedAt = DateTime.Now; ;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}
