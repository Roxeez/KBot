using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Battle;
using KBot.Event.Entities;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Network.Packet.Entities;

namespace KBot.Network.Processor.Entities
{
    public class GetProcessor : PacketProcessor<Get>
    {
        private readonly EventPipeline eventPipeline;

        public GetProcessor(EventPipeline eventPipeline)
        {
            this.eventPipeline = eventPipeline;
        }

        protected override void Process(GameSession session, Get packet)
        {
            Map map = session.Character.Map;
            LivingEntity entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            MapObject drop = map.GetEntity<MapObject>(EntityType.MapObject, packet.DropId);

            if (drop == null)
            {
                Log.Warning($"Can't found drop with ID {packet.DropId} who has been picked up");
                return;
            }
            
            map.RemoveEntity(drop);
            
            eventPipeline.Process(session, new EntityPickupEvent
            {
                Drop = drop,
                Entity = entity
            });
        }
    }
}