using System.Linq;
using KBot.Event;
using KBot.Event.Maps;
using KBot.Game;
using KBot.Game.Entities;

namespace KBot.Core.Event.Processor
{
    public class EntityJoinProcessor : EventProcessor<EntityJoinEvent>
    {
        private readonly Bot bot;

        public EntityJoinProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, EntityJoinEvent e)
        {
            if (!(e.Entity is Monster monster))
            {
                return;
            }

            bool exists = bot.Monsters.Any(x => x.ModelId == monster.ModelId);
            if (exists)
            {
                return;
            }

            bot.Monsters.Add(monster);
        }
    }
}