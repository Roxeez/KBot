using KBot.Common.Extension;
using KBot.Event;
using KBot.Event.Maps;
using KBot.Game;

namespace KBot.Core.Event.Processor
{
    public class MapChangeProcessor : EventProcessor<MapChangeEvent>
    {
        private readonly Bot bot;

        public MapChangeProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, MapChangeEvent e)
        {
            if (e.Source?.Id == e.Destination.Id)
            {
                return;
            }

            bot.Stop();
            
            bot.Monsters.Clear();
            bot.WhitelistedMonsters.Clear();
            
            bot.Path.Clear();
            bot.IsRecordingPath = false;

            bot.MapPreview = bot.Session.Character.Map.Preview.ToBitmapSource();
        }
    }
}