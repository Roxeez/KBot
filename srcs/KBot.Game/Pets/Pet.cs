using KBot.Game.Entities;
using KBot.Interop;

namespace KBot.Game.Pets
{
    public class Pet
    {
        private readonly OwnedPet pet;
        
        public int Id => pet.Id;
        public int Loyalty => pet.Loyalty;

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

        private readonly CharacterBridge bridge = new CharacterBridge();
        
        public Pet(OwnedPet pet, LivingEntity entity)
        {
            this.pet = pet;
            Entity = entity;
        }

        public void Walk(Position position)
        {
            if (!Entity.Map.IsWalkable(position))
            {
                return;
            }
            
            bridge.PetWalk(position.X, position.Y);
        }
    }
}