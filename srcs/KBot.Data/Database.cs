using System;
using System.Collections.Generic;
using KBot.Common;
using KBot.Extension;

namespace KBot.Game.Data
{
    public class Database
    {
        private Dictionary<int, MonsterData> monsters;
        private Dictionary<int, ItemData> items;
        private Dictionary<int, MapData> maps;

        private readonly FileManager fileManager;
        
        public Database(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }
        
        public MonsterData GetMonsterData(int modelId)
        {
            if (monsters == null)
            {
                throw new InvalidOperationException("Database is not correctly loaded (missing monsters)");
            }

            return monsters.GetValue(modelId);
        }

        public ItemData GetItemData(int modelId)
        {
            if (items == null)
            {
                throw new InvalidOperationException("Database is not correctly loaded (missing items)");
            }

            return items.GetValue(modelId);
        }

        public MapData GetMapData(int mapId)
        {
            if (maps == null)
            {
                throw new InvalidOperationException("Database is not correctly loaded (missing maps)");
            }

            return maps.GetValue(mapId);
        }
        
        public void Load()
        {
            monsters = fileManager.Load<Dictionary<int, MonsterData>>("database/monsters.json");
            items = fileManager.Load<Dictionary<int, ItemData>>("database/items.json");
            maps = fileManager.Load<Dictionary<int, MapData>>("database/maps.json");
        }
    }
}