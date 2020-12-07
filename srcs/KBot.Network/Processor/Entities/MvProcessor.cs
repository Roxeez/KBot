using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Network.Packet.Entities;

namespace KBot.Network.Processor.Entities
{
    public class MvProcessor : PacketProcessor<Mv>
    {
        protected override void Process(GameSession session, Mv packet)
        {
            Map map = session.Character.Map;

            LivingEntity entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                Log.Warning($"Can't found entity {packet.EntityType} with ID {packet.EntityId} when processing entity movement");
                return;
            }

            entity.Position = packet.Position;
            entity.Speed = packet.Speed;
        }
    }
}