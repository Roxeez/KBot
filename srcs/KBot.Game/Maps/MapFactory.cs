using System;
using System.Drawing;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Data;
using KBot.Data.Translation;
using KBot.Game.Maps;

namespace KBot.Game.Maps
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
            string name = languageService.GetTranslation(TranslationCategory.Map, data.NameKey);
            Bitmap preview = database.GetImage(ImageType.Map, mapId);
            
            return new Map(mapId, name, data.Grid, preview);
        }
    }
}