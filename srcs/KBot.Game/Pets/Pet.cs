using System;
using System.Threading;
using KBot.Common.Logging;
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

        public int BasicRange
        {
            get => Entity.BasicRange;
        }

        public int BasicCastTime
        {
            get => Entity.BasicCastTime;
        }

        public int BasicCooldown
        {
            get => Entity.BasicCooldown;
        }

        public int Level
        {
            get => Entity.Level;
            set => Entity.Level = value;
        }
        
        public Npc Entity { get; }
        
        public Character Owner { get; }

        private static readonly PetBridge Bridge = new PetBridge();

        public Pet(Character character, OwnedPet pet, Npc entity)
        {
            this.pet = pet;
            
            Owner = character;
            Entity = entity;
        }

        public void Walk(Position destination)
        {
            Bridge.Walk(destination.X, destination.Y);
            Log.Debug($"Pet walking to {destination}");
        }
    }
}