using KBot.Common.Logging;
using KBot.Game.Entities;
using KBot.Interop;

namespace KBot.Game.Pets
{
    public class Partner
    {
        private readonly OwnedPartner partner;
        
        public int Id => partner.Id;

        public Position Position
        {
            get => Entity.Position;
            set => Entity.Position = value;
        }

        public int Speed
        {
            get => Entity.Speed;
            set => Entity.Speed = value;
        }
        
        public string Name
        {
            get => Entity.Name;
            set => Entity.Name = value;
        }

        public int Level
        {
            get => Entity.Level;
            set => Entity.Level = value;
        }
        
        public LivingEntity Entity { get; }

        // private static readonly PetBridge Bridge = new PetBridge();
        
        public Partner(OwnedPartner partner, LivingEntity entity)
        {
            this.partner = partner;
            Entity = entity;
        }

        public void Walk(Position position)
        {

        }
    }
}