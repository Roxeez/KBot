using System.Linq;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Game.Battle;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class SuProcessor : PacketProcessor<Su>
    {
        protected override void Process(GameSession session, Su packet)
        {
            Map map = session.Character.Map;
            
            LivingEntity caster = map.GetEntity<LivingEntity>(packet.CasterType, packet.CasterId);
            LivingEntity target = map.GetEntity<LivingEntity>(packet.TargetType, packet.TargetId);

            if (caster is Character character)
            {
                Skill skill = character.Skills.FirstOrDefault(x => x.Id == packet.SkillId);
                if (skill != null)
                {
                    skill.IsOnCooldown = true;
                }
            }

            if (caster == null || target == null)
            {
                Log.Warning("Can't found target or caster in map when processing damage");
                return;
            }

            target.HpPercentage = packet.HpPercentage > 100 ? 100 : packet.HpPercentage;

            if (!packet.IsTargetAlive)
            {
                target.HpPercentage = 0;
                map.RemoveEntity(target);
                
                Log.Debug($"Entity {target.EntityType} with ID {target.Id} has been killed by {target.EntityType} with ID {target.Id}");
            }
        }
    }
}