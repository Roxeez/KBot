using System.Threading;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Game;
using KBot.Game.Extension;
using KBot.Game.Pets;

namespace KBot.Core.Event.Processor
{
    public class WalkToWaypointProcessor : EventProcessor<WalkToWaypointEvent>
    {
        private readonly Bot bot;

        public WalkToWaypointProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, WalkToWaypointEvent e)
        {
            if (session.Character.Position.Equals(e.Waypoint))
            {
                return;
            }
            
            Log.Information($"Walking to waypoint {e.Waypoint}");
            
            session.Character.Pet?.Walk(e.Waypoint);
            // session.Character.Partner?.Walk(e.Waypoint);
            session.Character.WalkTo(e.Waypoint);
        }
    }
}