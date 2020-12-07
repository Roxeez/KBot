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
            return other != null && other.Id == Id;
        }
    }
}