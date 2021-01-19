using System.Drawing;
using System.Windows.Media.Imaging;
using KBot.Common.Extension;
using KBot.Data;
using KBot.Data.Translation;
using KBot.Game.Enum;

namespace KBot.Game.Inventories
{
    public class ItemFactory
    {
        private readonly Database database;
        private readonly LanguageService languageService;

        public ItemFactory(Database database, LanguageService languageService)
        {
            this.database = database;
            this.languageService = languageService;
        }

        public Item CreateItem(int modelId)
        {
            ItemData data = database.GetItemData(modelId);
            string name = languageService.GetTranslation(TranslationCategory.Item, data.NameKey);
            Bitmap icon = database.GetImage(ImageType.Icon, data.Icon);

            return new Item(modelId, name, (InventoryType)data.InventoryType, icon.ToBitmapSource())
            {
                Type = data.Type,
                SubType = data.SubType
            };
        }
    }
}