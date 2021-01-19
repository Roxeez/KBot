using System.Linq;
using System.Threading;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Game.Extension;

namespace KBot.Core.Event.Processor
{
    public class UseBuffsProcessor : EventProcessor<UseBuffsEvent>
    {
        private readonly Bot bot;

        public UseBuffsProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, UseBuffsEvent e)
        {
            Log.Information("Using all buffs");
            foreach (Skill skill in bot.UsedBuffSkills.ToList())
            {
                if (skill.IsOnCooldown() || !bot.IsRunning)
                {
                    continue;
                }

                session.Character.Attack(session.Character, skill);
                Thread.Sleep((skill.CastTime * 100) + 1000);
            }
        }
    }
}