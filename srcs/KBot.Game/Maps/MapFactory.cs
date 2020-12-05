using KBot.Data;
using KBot.Data.Translation;
using KBot.Game.Maps;

namespace KBot.Game.Factory
{
    public sealed class MapFactory
    {
        private readonly Database database;
        private readonly LanguageService languageService;

        public MapFactory(Database database, LanguageService languageService)
        {
            this.database = database;
            this.languageService = languageService;
        }
        
        public Map CreateMap(int mapId)
        {
            MapData data = database.GetMapData(mapId);
            if (data == null)
            {
                return new Map(mapId, new byte[4])
                {
                    Name = "UNKNOWN"
                };
            }

            string name = languageService.GetTranslation(TranslationCategory.Map, data.NameKey);
            return new Map(mapId, data.Grid)
            {
                Name = name
            };
        }
    }
}