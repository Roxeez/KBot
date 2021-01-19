using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private Dictionary<int, BuffData> buffs;

        public const string MonsterPath = "db/monsters.json";
        public const string ItemPath = "db/items.json";
        public const string MapPath = "db/maps.json";
        public const string SkillPath = "db/skills.json";
        public const string BuffPath = "db/buffs.json";
        public const string IconPath = "db/images/icons";
        public const string MapPreviewPath = "db/images/maps";
        
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

        public BuffData GetBuffData(int buffId)
        {
            if (buffs == null)
            {
                throw new DatabaseException("Database is not correctly loaded (missing buffs)");
            }
            
            return buffs.GetValue(buffId) ?? throw new DatabaseException($"Failed to get buff {buffId} from database (database update required)");
        }

        public Bitmap GetImage(ImageType type, int id)
        {
            switch (type)
            {
                case ImageType.Icon:
                    if (!fileManager.HasFile($"{IconPath}/{id}"))
                    {
                        return fileManager.LoadBitmap($"{IconPath}/1");
                    }
                    return fileManager.LoadBitmap($"{IconPath}/{id}");
                case ImageType.Map:
                    return fileManager.LoadBitmap($"{MapPreviewPath}/{id}");
                default:
                    throw new DatabaseException($"Can't found image {type} with ID {id}");
            }
        }

        public void Load()
        {
            monsters = fileManager.Load<Dictionary<int, MonsterData>>(MonsterPath);
            items = fileManager.Load<Dictionary<int, ItemData>>(ItemPath);
            maps = fileManager.Load<Dictionary<int, MapData>>(MapPath);
            skills = fileManager.Load<Dictionary<int, SkillData>>(SkillPath);
            buffs = fileManager.Load<Dictionary<int, BuffData>>(BuffPath);
        }

        public bool CanBeLoaded()
        {
            return fileManager.HasFile(MonsterPath) 
                && fileManager.HasFile(ItemPath) 
                && fileManager.HasFile(MapPath) 
                && fileManager.HasFile(SkillPath)
                && fileManager.HasFile(BuffPath)
                && fileManager.HasDirectory(IconPath)
                && fileManager.HasDirectory(MapPreviewPath);
        }
    }
}