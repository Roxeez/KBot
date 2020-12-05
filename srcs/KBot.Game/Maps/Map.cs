using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KBot.Common.Collection;
using KBot.Common.Extension;
using KBot.Game.Entities;
using KBot.Game.Enum;

namespace KBot.Game.Maps
{
    public class Map
    {
        public int Id { get; }
        public string Name { get; set; }
        public byte[] Grid { get; }
        public int Height { get; }
        public int Width { get; }
        
        public ObservableDictionary<long, Monster> Monsters { get; }
        public ObservableDictionary<long, Npc> Npcs { get; }
        public ObservableDictionary<long, Player> Players { get; }
        public ObservableDictionary<long, MapObject> MapObjects { get; }
        public ObservableDictionary<long, Portal> Portals { get; }

        private byte this[int x, int y] => Grid.Skip(4 + y * Width + x).Take(1).FirstOrDefault();

        public Map(int id, byte[] grid)
        {
            Id = id;
            Grid = grid;
            Monsters = new ObservableDictionary<long, Monster>();
            Npcs = new ObservableDictionary<long, Npc>();
            Players = new ObservableDictionary<long, Player>();
            MapObjects = new ObservableDictionary<long, MapObject>();
            Portals = new ObservableDictionary<long, Portal>();
            
            Width = Grid.Length == 0 ? 0 : BitConverter.ToInt16(Grid.Take(2).ToArray(), 0);
            Height = Grid.Length == 0 ? 0 : BitConverter.ToInt16(Grid.Skip(2).Take(2).ToArray(), 0);
        }

        public Entity GetEntity(EntityType entityType, long entityId)
        {
            switch (entityType)
            {
                case EntityType.Monster:
                    return Monsters.GetValue(entityId);
                case EntityType.Npc:
                    return Npcs.GetValue(entityId);
                case EntityType.Player:
                    return Npcs.GetValue(entityId);
                case EntityType.MapObject:
                    return MapObjects.GetValue(entityId);
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "Incorrect entity type");
            }
        }

        public bool IsWalkable(Position position)
        {
            if (Grid.Length == 0)
            {
                return true;
            }
            
            if (position.X > Width || position.X < 0 || position.Y > Height || position.Y < 0)
            {
                return false;
            }

            byte value = this[position.X, position.Y];

            return value == 0 || value == 2 || value >= 16 && value <= 19;
        }
    }
}