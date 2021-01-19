using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Network.Packet.Characters;

namespace KBot.Network.Processor.Characters
{
    public class StatProcessor : PacketProcessor<Stat>
    {
        private readonly EventPipeline eventPipeline;

        public StatProcessor(EventPipeline eventPipeline)
        {
            this.eventPipeline = eventPipeline;
        }

        protected override void Process(GameSession session, Stat packet)
        {
            Character character = session.Character;

            character.Hp = packet.Hp;
            character.Mp = packet.Mp;
            character.HpMaximum = packet.HpMaximum;
            character.MpMaximum = packet.MpMaximum;

            character.HpPercentage = character.Hp.GetPercentage(character.HpMaximum);
            character.MpPercentage = character.Mp.GetPercentage(character.MpMaximum);
            
            eventPipeline.Process(session, new StatChangeEvent());
            Log.Trace("Character hp/mp updated completed");
        }
    }
}