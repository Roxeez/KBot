using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using KBot.Common.Logging;
using KBot.Game.Enum;
using KBot.Game.Group;
using KBot.Game.Skills;
using KBot.Interop;

namespace KBot.Game.Entities
{
    public sealed class Character : Player
    {
        public int JobLevel { get; set; }
        
        public int Experience { get; set; }
        public int JobExperience { get; set; }
        public int HeroExperience { get; set; }

        public int Hp { get; set; }
        public int HpMaximum { get; set; }
        public int Mp { get; set; }
        public int MpMaximum { get; set; }

        public ObservableCollection<Skill> Skills { get; }
        
        public Partner Partner { get; set; }
        public Pet Pet { get; set; }


        private readonly GameSession session;
        private readonly CharacterBridge bridge;
        
        public Character(GameSession session)
        {
            this.session = session;
            this.bridge = new CharacterBridge();
            
            Skills = new ObservableCollection<Skill>();
        }

        public void Walk(Position position)
        {
            if (!Map.IsWalkable(position))
            {
                return;
            }

            bridge.Walk(position.X, position.Y);
        }

        public void Attack(Skill skill, LivingEntity entity)
        {
            if (!Skills.Contains(skill))
            {
                Log.Warning("Trying to use a skill not present in character skills");
                return;
            }

            switch (skill.Target)
            {
                case SkillTarget.Self:
                    if (!entity.Equals(this))
                    {
                        Log.Warning($"Trying to use skill on another entity but skill target should be self");
                        return;
                    }
                    break;
                case SkillTarget.Target:
                    if (entity.Equals(this))
                    {
                        Log.Warning("Trying to use skill on self but skill need a target.");
                        return;
                    }
                    break;
                case SkillTarget.NoTarget:
                    Log.Warning("Trying to use a skill without target on a target");
                    return;
            }

            if (!Position.IsInRange(entity.Position, skill.Range))
            {
                Log.Warning($"Trying to attack entity at {entity.Position} from {Position} but it's out of skill range ({skill.Range} cells)");
                return;
            }

            session.SendPacket($"u_s {skill.CastId} {(int)entity.EntityType} {entity.Id}");
        }

        public void Attack(Skill skill, Position position)
        {
            if (!Skills.Contains(skill))
            {
                Log.Warning("Trying to use a skill not present in character skills");
                return;
            }

            if (skill.Target != SkillTarget.NoTarget)
            {
                Log.Warning($"Trying to use a skill at defined position when target should be {skill.Target}");
                return;
            }
            
            if (!Position.IsInRange(position, skill.Range))
            {
                Log.Warning($"Trying to attack at {position} from {Position} but it's out of skill range ({skill.Range} cells)");
                return;
            }
        }
    }
}