using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Entities;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Network.Packet.Entities;

namespace KBot.Network.Processor.Entities
{
    public class MvProcessor : PacketProcessor<Mv>
    {
        private readonly EventPipeline eventPipeline;

        public MvProcessor(EventPipeline eventPipeline)
        {
            this.eventPipeline = eventPipeline;
        }

        protected override void Process(GameSession session, Mv packet)
        {
            Map map = session.Character.Map;

            LivingEntity entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                Log.Trace($"Can't found entity {packet.EntityType} with ID {packet.EntityId} when processing entity movement");
                return;
            }

            Position from = entity.Position;

            entity.Position = packet.Position;
            entity.Speed = packet.Speed;
            
            eventPipeline.Process(session, new EntityMoveEvent
            {
                Entity = entity,
                From = from,
                To = packet.Position
            });
        }
    }
}