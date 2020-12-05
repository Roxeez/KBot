using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KBot.Common.Collection;
using KBot.Game.Entities;

namespace KBot.Game.Maps
{
    public class Map
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Grid { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        
        public ObservableDictionary<long, Monster> Monsters { get; }
        public ObservableDictionary<long, Npc> Npcs { get; }
        public ObservableDictionary<long, Player> Players { get; }
        public ObservableDictionary<long, MapObject> MapObjects { get; }
        public ObservableDictionary<long, Portal> Portals { get; }

        private byte this[int x, int y] => Grid.Skip(4 + y * Width + x).Take(1).FirstOrDefault();

        public Map()
        {
            Monsters = new ObservableDictionary<long, Monster>();
            Npcs = new ObservableDictionary<long, Npc>();
            Players = new ObservableDictionary<long, Player>();
            MapObjects = new ObservableDictionary<long, MapObject>();
            Portals = new ObservableDictionary<long, Portal>();
        }
        
        public bool IsWalkable(Position position)
        {
            if (position.X > Width || position.X < 0 || position.Y > Height || position.Y < 0)
            {
                return false;
            }

            byte value = this[position.X, position.Y];

            return value == 0 || value == 2 || value >= 16 && value <= 19;
        }
    }
}