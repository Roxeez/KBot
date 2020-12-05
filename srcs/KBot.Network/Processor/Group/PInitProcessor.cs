using System.Linq;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Group;
using KBot.Game.Maps;
using KBot.Network.Packet.Group;

namespace KBot.Network.Processor.Group
{
    public class PInitProcessor : PacketProcessor<PInit>
    {
        protected override void Process(GameSession session, PInit packet)
        {
            Character character = session.Character;
            Map map = session.Character.Map;
            
            foreach (PInitSub member in packet.Members)
            {
                LivingEntity entity = map.GetEntity<LivingEntity>(member.EntityType, member.EntityId);
                if (entity == null)
                {
                    Log.Warning($"Can't found entity {member.EntityType} with ID {member.EntityId} for party");
                    continue;
                }

                if (entity.EntityType == EntityType.Npc)
                {
                    if (member.IsPartner)
                    {
                        character.Partner = new Partner(entity);
                    }
                    else
                    {
                        character.Pet = new Pet(entity);
                    }
                }
            }
        }
    }
}