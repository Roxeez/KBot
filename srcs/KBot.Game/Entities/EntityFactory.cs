using KBot.Data;
using KBot.Data.Translation;
using KBot.Game.Inventories;

namespace KBot.Game.Entities
{
    public sealed class EntityFactory
    {
        private readonly Database database;
        private readonly LanguageService languageService;
        private readonly ItemFactory itemFactory;

        public EntityFactory(Database database, LanguageService languageService, ItemFactory itemFactory)
        {
            this.database = database;
            this.languageService = languageService;
            this.itemFactory = itemFactory;
        }
        
        public Monster CreateMonster(int modelId, long entityId)
        {
            MonsterData data = database.GetMonsterData(modelId);
            string name = languageService.GetTranslation(TranslationCategory.Monster, data.NameKey);
            
            return new Monster(entityId, name, modelId)
            {
                Level = data.Level
            };
        }

        public Npc CreateNpc(int modelId, long entityId, string name)
        {
            MonsterData data = database.GetMonsterData(modelId);
             
            name = name == "@" || name == "-" ? languageService.GetTranslation(TranslationCategory.Monster, data.NameKey) : name;
            
            return new Npc(entityId, name, modelId)
            {
                Level = data.Level,
                BasicRange = data.BasicAttackRange,
                BasicCastTime = data.BasicAttackCastTime,
                BasicCooldown = data.BasicAttackCooldown
            };
        }

        public MapObject CreateMapObject(int modelId, long entityId, int amount = 1)
        {
            Item item = itemFactory.CreateItem(modelId);
            return new MapObject(entityId, new ItemStack(item, amount));
        }
    }
}