using System.Linq;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class BfProcessor : PacketProcessor<Bf>
    {
        private readonly BuffFactory buffFactory;

        public BfProcessor(BuffFactory buffFactory)
        {
            this.buffFactory = buffFactory;
        }
        
        protected override void Process(GameSession session, Bf packet)
        {
            LivingEntity entity = session.Character.Map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                Log.Warning($"Can't found entity {packet.EntityType} with ID {packet.EntityId} to remove buff {packet.BuffId}");
                return;
            }
            
            if (packet.Duration == 0)
            {
                Buff existing = entity.Buffs.FirstOrDefault(x => x.Id == packet.BuffId);
                if (existing != null)
                {
                    entity.Buffs.Remove(existing);
                    Log.Debug($"Buff {existing.Id} removed from {entity.EntityType} with ID {entity.Id}");
                }
            }
            else
            {
                Buff buff = buffFactory.CreateBuff(packet.BuffId);
                Buff similarBuff = entity.Buffs.FirstOrDefault(x => x.GroupId == buff.GroupId);

                if (similarBuff != null)
                {
                    if (similarBuff.Level > buff.Level)
                    {
                        Log.Debug($"Ignoring buff {buff.Id} because a more powerful buff is already on entity");
                        return;
                    }

                    entity.Buffs.Remove(similarBuff);
                    Log.Debug($"Buff {similarBuff.Id} removed from {entity.EntityType} with ID {entity.Id}");
                }

                entity.Buffs.Add(buff);
                Log.Debug($"Buff {buff.Id} added to {entity.EntityType} with ID {entity.Id}");
            }
        }
    }
}