using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Network.Packet.Characters;

namespace KBot.Network.Processor.Characters
{
    public class WalkProcessor : PacketProcessor<Walk>
    {
        private readonly EventPipeline eventPipeline;

        public WalkProcessor(EventPipeline eventPipeline)
        {
            this.eventPipeline = eventPipeline;
        }

        protected override void Process(GameSession session, Walk packet)
        {
            Character character = session.Character;

            Position from = character.Position;
            Position to = packet.Position;

            // character.Position = to;
            character.Speed = packet.Speed;

            eventPipeline.Process(session, new WalkEvent
            {
                From = from,
                To = to
            });
        }
    }
}