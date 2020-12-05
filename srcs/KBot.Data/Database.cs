using System;
using System.Collections.Generic;
using KBot.Common;
using KBot.Common.Extension;

namespace KBot.Data
{
    public class Database
    {
        private Dictionary<int, MonsterData> monsters;
        private Dictionary<int, ItemData> items;
        private Dictionary<int, MapData> maps;
        private Dictionary<int, SkillData> skills;

        public const string MonsterPath = "db/monsters.json";
        public const string ItemPath = "db/items.json";
        public const string MapPath = "db/maps.json";
        public const string SkillPath = "db/skills.json";
        
        private readonly FileManager fileManager;
        
        public Database(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }
        
        public MonsterData GetMonsterData(int modelId)
        {
            if (monsters == null)
            {
                throw new DatabaseException("Database is not correctly loaded (missing monsters)");
            }

            return monsters.GetValue(modelId) ?? throw new DatabaseException($"Failed to get monster {modelId} from database (database update required)");
        }

        public SkillData GetSkillData(int skillId)
        {
            if (skills == null)
            {
                throw new DatabaseException("Database is not correctly loaded (missing skills)");
            }

            return skills.GetValue(skillId) ?? throw new DatabaseException($"Failed to get skill {skillId} from database (database update required)");
        }

        public ItemData GetItemData(int modelId)
        {
            if (items == null)
            {
                throw new DatabaseException("Database is not correctly loaded (missing items)");
            }

            return items.GetValue(modelId) ?? throw new DatabaseException($"Failed to get item {modelId} from database (database update required)");
        }

        public MapData GetMapData(int mapId)
        {
            if (maps == null)
            {
                throw new DatabaseException("Database is not correctly loaded (missing maps)");
            }

            return maps.GetValue(mapId) ?? throw new DatabaseException($"Failed to get map {mapId} from database (database update required)");
        }
        
        public void Load()
        {
            monsters = fileManager.Load<Dictionary<int, MonsterData>>(MonsterPath);
            items = fileManager.Load<Dictionary<int, ItemData>>(ItemPath);
            maps = fileManager.Load<Dictionary<int, MapData>>(MapPath);
            skills = fileManager.Load<Dictionary<int, SkillData>>(SkillPath);
        }

        public bool CanBeLoaded()
        {
            return fileManager.HasFile(MonsterPath) && fileManager.HasFile(ItemPath) && fileManager.HasFile(MapPath) && fileManager.HasFile(SkillPath);
        }
    }
}