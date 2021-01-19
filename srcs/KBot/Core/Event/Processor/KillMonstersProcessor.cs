using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KBot.Common.Logging;
using KBot.Event;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Game.Entities;
using KBot.Game.Extension;

namespace KBot.Core.Event.Processor
{
    public class KillMonstersProcessor : EventProcessor<KillMonstersEvent>
    {
        private readonly Bot bot;

        public KillMonstersProcessor(Bot bot)
        {
            this.bot = bot;
        }
        
        protected override void Process(GameSession session, KillMonstersEvent e)
        {
            Character character = session.Character;

            var monsters = new List<Monster>();
            foreach (Monster monster in character.Map.Monsters.Values.ToList())
            {
                double distance = monster.Position.GetDistance(e.Waypoint);
                if (distance <= 14)
                {
                    bool whitelisted = bot.WhitelistedMonsters.Any(x => x.ModelId == monster.ModelId);
                    if (!whitelisted)
                    {
                        continue;
                    }
                
                    monsters.Add(monster);
                }
            }
            
            if (!monsters.Any())
            {
                Log.Information("No monster found, skipping killing process");
                return;
            }

            Log.Information($"Killing {monsters.Count} monsters");
            foreach (Monster monster in monsters)
            {
                while (monster.HpPercentage > 0 && bot.IsRunning)
                {
                    Skill skill = character.ComboSkill;
                    if (skill == null || skill.IsOnCooldown())
                    {
                        skill = bot.UsedDamageSkills.FirstOrDefault(x => !x.IsOnCooldown());
                    }
                
                    if (skill == null)
                    {
                        skill = character.GetBasicAttack();
                    }

                    if (skill.IsOnCooldown())
                    {
                        continue;
                    }

                    if (character.Pet != null)
                    {
                        if (!character.Pet.Position.Equals(monster.Position))
                        {
                            character.Pet.WalkTo(monster);
                        }
                    }

                    int skillRange = character.GetSkillRange(skill);
                    if (!character.IsInRange(monster.Position, skillRange))
                    {
                        character.WalkInRange(monster.Position, skillRange);
                    }

                    if (character.CantAttack)
                    {
                        Log.Information("Character is stunned");
                        continue;
                    }

                    character.Attack(monster, skill);
                    Thread.Sleep((skill.CastTime * 200) + 800);
                }
            }
        }
    }
}