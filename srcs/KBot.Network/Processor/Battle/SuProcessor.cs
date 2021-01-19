using System;
using System.Linq;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Battle;
using KBot.Event.Maps;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Game.Battle;
using KBot.Game.Enum;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class SuProcessor : PacketProcessor<Su>
    {
        private readonly EventPipeline eventPipeline;

        public SuProcessor(EventPipeline eventPipeline)
        {
            this.eventPipeline = eventPipeline;
        }
        
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
                    // skill.LastUse = DateTime.Now;
                }
            }

            if (caster == null || target == null)
            {
                Log.Warning("Can't found target or caster in map when processing damage");
                return;
            }

            target.HpPercentage = packet.HpPercentage > 100 ? 100 : packet.HpPercentage;

            eventPipeline.Process(session, new EntityDamageEvent
            {
                Caster = caster,
                Target = target,
                Damage = packet.Damage
            });
            
            if (!packet.IsTargetAlive)
            {
                target.HpPercentage = 0;
                eventPipeline.Process(session, new EntityDeathEvent
                {
                    Entity = target,
                    Killer = caster
                });
                
                if (session.Character.Pet != null)
                {
                    if (session.Character.Pet.Entity.Equals(target))
                    {
                        session.Character.Pet = null;
                    }
                }
                
                map.RemoveEntity(target);
                
                eventPipeline.Process(session, new EntityLeaveEvent
                {
                    Entity = target,
                    Map = map
                });
                
                Log.Debug($"Entity {target.EntityType} with ID {target.Id} has been killed by {caster.EntityType} with ID {caster.Id}");
            }
        }
    }
}