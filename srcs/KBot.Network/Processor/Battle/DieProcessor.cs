using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class DieProcessor : PacketProcessor<Die>
    {
        protected override void Process(GameSession session, Die packet)
        {
            Map map = session.Character.Map;

            LivingEntity entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                Log.Warning($"Can't found entity {packet.EntityType} with ID {packet.EntityId} when processing die");
                return;
            }

            entity.HpPercentage = 0;
            map.RemoveEntity(entity);
            
            Log.Warning($"Entity {entity.EntityType} with ID {entity.Id} died from unknown source");
        }
    }
}