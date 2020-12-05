using System;
using KBot.Common.Logging;
using KBot.Data;
using KBot.Data.Translation;
using KBot.Game.Entities;

namespace KBot.Game.Entities
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
            string name = languageService.GetTranslation(TranslationCategory.Item, data.NameKey);
            
            return new MapObject
            {
                Name = name,
                ModelId = modelId
            };
        }
    }
}