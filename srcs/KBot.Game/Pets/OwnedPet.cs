using System;

namespace KBot.Game.Pets
{
    public class OwnedPet : IEquatable<OwnedPet>
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public long EntityId { get; set; } 
        public int Level { get; set; }
        public int Loyalty { get; set; }
        public bool IsTeamMember { get; set; }
        public string Name { get; set; }
        public bool IsSummonable { get; set; }


        public bool Equals(OwnedPet other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id && EntityId == other.EntityId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((OwnedPet)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ EntityId.GetHashCode();
            }
        }
    }
}