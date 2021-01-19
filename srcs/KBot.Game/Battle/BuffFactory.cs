using KBot.Data;
using KBot.Data.Translation;
using KBot.Game.Enum;

namespace KBot.Game.Battle
{
    public class BuffFactory
    {
        private readonly Database database;
        private readonly LanguageService languageService;

        public BuffFactory(Database database, LanguageService languageService)
        {
            this.database = database;
            this.languageService = languageService;
        }

        public Buff CreateBuff(int id, int duration)
        {
            BuffData data = database.GetBuffData(id);
            string name = languageService.GetTranslation(TranslationCategory.Buff, data.NameKey);
            
            return new Buff(id, name, data.GroupId, (BuffCategory)data.Category, (BuffEffect)data.Effect, data.Level, duration);
        }
    }
}