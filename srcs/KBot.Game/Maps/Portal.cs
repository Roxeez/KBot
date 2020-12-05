using System;
using KBot.Game.Enum;

namespace KBot.Game.Maps
{
    public sealed class Portal : IEquatable<Portal>
    {
        public Map Map { get; set; }
        public int Id { get; set; }
        public int DestinationId { get; set; }
        public Position Position { get; set; }
        public PortalType Type { get; set; }
        
        public bool Equals(Portal other)
        {
            return other != null && other.Id == Id && other.DestinationId == DestinationId && Position.Equals(other.Position);
        }
    }
}