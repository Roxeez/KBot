using KBot.Common.Logging;
using KBot.Data;
using KBot.Data.Translation;
using KBot.Game.Entities;

namespace KBot.Game.Factory
{
    public sealed class EntityFactory
    {
        private readonly Database database;
        private readonly LanguageService languageService;

        public EntityFactory(Database database, LanguageService languageService)
        {
            this.database = database;
            this.languageService = languageService;
        }
        
        public Monster CreateMonster(int modelId)
        {
            MonsterData data = database.GetMonsterData(modelId);
            if (data == null)
            {
                Log.Warning($"Can't found monster {modelId} in database");
                return new Monster
                {
                    Name = "UNKNOWN",
                    Level = 1,
                    ModelId = modelId,
                };
            }

            string name = languageService.GetTranslation(TranslationCategory.Monster, data.NameKey);
            return new Monster
            {
                Name = name,
                Level = data.Level,
                ModelId = modelId
            };
        }

        public Npc CreateNpc(int modelId)
        {
            MonsterData data = database.GetMonsterData(modelId);
            if (data == null)
            {
                Log.Warning($"Can't found monster {modelId} in database");
                return new Npc
                {
                    Name = "UNKNOWN",
                    Level = 1,
                    ModelId = modelId,
                };
            }

            string name = languageService.GetTranslation(TranslationCategory.Monster, data.NameKey);
            return new Npc
            {
                Name = name,
                Level = data.Level,
                ModelId = modelId
            };
        }

        public MapObject CreateMapObject(int modelId)
        {
            ItemData data = database.GetItemData(modelId);
            if (data == null)
            {
                return new MapObject
                {
                    Name = "UNKNOWN",
                    ModelId = modelId
                };
            }
            
            string name = languageService.GetTranslation(TranslationCategory.Item, data.NameKey);
            return new MapObject
            {
                Name = name,
                ModelId = modelId
            };
        }
    }
}