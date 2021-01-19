using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KBot.Event;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Game.Entities;
using KBot.Game.Extension;

namespace KBot.Core.Event.Processor
{
    public class PickUpDropsProcessor : EventProcessor<PickUpDropsEvent>
    {
        private readonly Bot bot;

        public PickUpDropsProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, PickUpDropsEvent e)
        {
            Character character = session.Character;
            IEnumerable<MapObject> drops = character.Map.MapObjects.Values.ToList()
                .FindAll(x => x.Position.GetDistance(e.Waypoint) <= 14)
                .Where(x => x.Owner == null || x.Owner.Equals(character));

            foreach (MapObject drop in drops)
            {
                if (bot.PickUpGolds && drop.ItemStack.Item.Id == 1046)
                {
                    character.WalkInRange(drop.Position, 1);
                    character.PickUp(drop);
                }

                if (bot.PickUpSoundFlowers && drop.ItemStack.Item.Id == 1086)
                {
                    Buff buff = character.Buffs.FirstOrDefault(x => x.Id == 378 || x.Id == 379);
                    if (buff == null || buff.TimeLeft.TotalMinutes <= 3)
                    {
                        character.WalkInRange(drop.Position, 1);
                        character.PickUp(drop);
                    }
                }
            }
        }
    }
}