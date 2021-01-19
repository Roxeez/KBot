using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;
using KBot.Common.Extension;
using KBot.Game.Entities;
using KBot.Game.Enum;

namespace KBot.Game.Maps
{
    /// <summary>
    /// Represent a Map in the game
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Id of this map
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Name of this map
        /// </summary>
        [NotNull]
        public string Name { get; }
        
        /// <summary>
        /// Grid of this map
        /// </summary>
        [NotNull]
        public byte[] Grid { get; }
        
        /// <summary>
        /// Height of this map
        /// </summary>
        public int Height { get; }
        
        /// <summary>
        /// Width of this map
        /// </summary>
        public int Width { get; }
        
        public Bitmap Preview { get; }
        
        /// <summary>
        /// Contains all monsters on this map
        /// </summary>
        [NotNull]
        public ConcurrentDictionary<long, Monster> Monsters { get; }
        
        /// <summary>
        /// Contains all npcs on this map
        /// </summary>
        [NotNull]
        public ConcurrentDictionary<long, Npc> Npcs { get; }
        
        /// <summary>
        /// Contains all players on this map
        /// </summary>
        [NotNull]
        public ConcurrentDictionary<long, Player> Players { get; }
        
        /// <summary>
        /// Contains all map objects on this map
        /// </summary>
        [NotNull]
        public ConcurrentDictionary<long, MapObject> MapObjects { get; }
        
        /// <summary>
        /// Contains all portals on this map
        /// </summary>
        [NotNull]
        public ConcurrentDictionary<long, Portal> Portals { get; }

        /// <summary>
        /// Contains all entities on this map
        /// </summary>
        [NotNull]
        public IEnumerable<Entity> Entities => Monsters.Values.Concat(Npcs.Values.Cast<Entity>()).Concat(Players.Values).Concat(MapObjects.Values);

        private byte this[int x, int y] => Grid.Skip(4 + y * Width + x).Take(1).FirstOrDefault();

        public Map(int id, string name, byte[] grid, Bitmap preview)
        {
            Id = id;
            Grid = grid;
            Name = name;
            Monsters = new ConcurrentDictionary<long, Monster>();
            Npcs = new ConcurrentDictionary<long, Npc>();
            Players = new ConcurrentDictionary<long, Player>();
            MapObjects = new ConcurrentDictionary<long, MapObject>();
            Portals = new ConcurrentDictionary<long, Portal>();
            Preview = preview;

            Width = Grid.Length == 0 ? 0 : BitConverter.ToInt16(Grid.Take(2).ToArray(), 0);
            Height = Grid.Length == 0 ? 0 : BitConverter.ToInt16(Grid.Skip(2).Take(2).ToArray(), 0);
        }

        /// <summary>
        /// Get entity with defined entity type and entity id
        /// </summary>
        /// <param name="entityType">Type of entity</param>
        /// <param name="entityId">Id of entity</param>
        /// <returns>Entity found or null if none</returns>
        [CanBeNull]
        public Entity GetEntity(EntityType entityType, long entityId)
        {
            switch (entityType)
            {
                case EntityType.Monster:
                    return Monsters.GetValue(entityId);
                case EntityType.Npc:
                    return Npcs.GetValue(entityId);
                case EntityType.Player:
                    return Players.GetValue(entityId);
                case EntityType.MapObject:
                    return MapObjects.GetValue(entityId);
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, "Incorrect entity type");
            }
        }

        /// <summary>
        /// Check if position is a walkable position
        /// </summary>
        /// <param name="position">Position you want to check</param>
        /// <returns>True if position is walkable, false if not</returns>
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