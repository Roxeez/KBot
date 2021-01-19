using System;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Maps;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Network.Packet.Maps;

namespace KBot.Network.Processor.Maps
{
    public class OutProcessor : PacketProcessor<Out>
    {
        private readonly EventPipeline eventPipeline;

        public OutProcessor(EventPipeline eventPipeline)
        {
            this.eventPipeline = eventPipeline;
        }

        protected override void Process(GameSession session, Out packet)
        {
            Map map = session.Character.Map;
            Entity entity = map.GetEntity(packet.EntityType, packet.EntityId);

            if (entity == null)
            {
                return;
            }

            if (session.Character.Pet != null)
            {
                if (session.Character.Pet.Entity.Equals(entity))
                {
                    session.Character.Pet = null;
                    Log.Debug("Pet is dead, removing it");
                }
            }
            
            map.RemoveEntity(entity);

            eventPipeline.Process(session, new EntityLeaveEvent
            {
                Entity = entity
            });
            
            Log.Debug($"Entity {packet.EntityType} with ID {packet.EntityId} removed from map");
        }
    }
}