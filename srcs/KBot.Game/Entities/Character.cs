using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using KBot.Common.Logging;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;
using KBot.Game.Pets;
using KBot.Game.Battle;
using KBot.Interop;

namespace KBot.Game.Entities
{
    /// <summary>
    /// Represent your character in the game
    /// </summary>
    public sealed class Character : Player, IDisposable
    {
        /// <summary>
        /// GameSession used by this character (used to send packet)
        /// </summary>
        public GameSession Session { get; }
        
        /// <summary>
        /// Current job level of this character
        /// </summary>
        public int JobLevel { get; set; }
        
        /// <summary>
        /// Current experience of this character (represented in percentage)
        /// </summary>
        public int Experience { get; set; }
        
        /// <summary>
        /// Current job experience of this character (represented in percentage)
        /// </summary>
        public int JobExperience { get; set; }
        
        /// <summary>
        /// Current hero experience of this character (represented in percentage)
        /// </summary>
        public int HeroExperience { get; set; }

        /// <summary>
        /// Current hp of this character
        /// </summary>
        public int Hp { get; set; }
        
        /// <summary>
        /// Current maximum hp of this character
        /// </summary>
        public int HpMaximum { get; set; }
        
        /// <summary>
        /// Current mp of this character
        /// </summary>
        public int Mp { get; set; }
        
        /// <summary>
        /// Current maximum mp of this character
        /// </summary>
        public int MpMaximum { get; set; }

        /// <summary>
        /// Contains all skills of this character (collection can't be null)
        /// </summary>
        public HashSet<Skill> Skills { get; }
        
        /// <summary>
        /// Current usable combo skill
        /// </summary>
        public Skill ComboSkill { get; set; }
        
        /// <summary>
        /// Contains all main inventories of this character (collection can't be null)
        /// </summary>
        public Dictionary<InventoryType, Inventory> Inventories { get; }
        
        /// <summary>
        /// Contains all pets of this character (collection can't be null)
        /// </summary>
        public HashSet<OwnedPet> Pets { get; }
        
        /// <summary>
        /// Contains all partners of this character (collection can't be null)
        /// </summary>
        public HashSet<OwnedPartner> Partners { get; }
        
        /// <summary>
        /// Current pet following character (can be null if none)
        /// </summary>
        public Pet Pet { get; set; }
        
        /// <summary>
        /// Current partner following character (can be null if none)
        /// </summary>
        public Partner Partner { get; set; }
        
        /// <summary>
        /// Bridge used for C#/C++ Interop
        /// </summary>
        private static readonly CharacterBridge Bridge = new CharacterBridge();

        private readonly Thread thread;
        private bool dispose;
        
        public Character(GameSession session) : base(0, string.Empty)
        {
            Session = session;
            
            Skills = new HashSet<Skill>();
            Inventories = new Dictionary<InventoryType, Inventory>();
            Pets = new HashSet<OwnedPet>();
            Partners = new HashSet<OwnedPartner>();

            thread = new Thread(() =>
            {
                while(!dispose)
                {
                    Position = new Position(Bridge.GetPositionX(), Bridge.GetPositionY());
                    Thread.Sleep(10);
                }
            });
            thread.Start();
        }

        /// <summary>
        /// Move to selected position
        /// </summary>
        /// <param name="position">Position where you want to move</param>
        public void Walk(Position position)
        {
            if (CantMove)
            {
                Log.Information("Character can't move (stunned)");
                return;
            }
            
            Bridge.Walk(position.X, position.Y);
            Log.Information($"Move character to {position}");
        }

        /// <summary>
        /// Use item on yourself
        /// </summary>
        /// <param name="stack">Item you want to use</param>
        public void UseItem(InventoryItem stack)
        {
            UseItemOn(stack, this);
        }

        public void PickUp(MapObject mapObject)
        {
            Session.SendPacket($"get {(int)EntityType} {Id} {mapObject.Id}");
        }

        /// <summary>
        /// Use item on defined entity
        /// </summary>
        /// <param name="item">Item you want to use</param>
        /// <param name="entity">Your target entity</param>
        public void UseItemOn(InventoryItem item, LivingEntity entity)
        {
            if (item.Stack.Amount == 0)
            {
                Log.Warning("Item amount is equal to 0");
                return;
            }

            if (!Map.HasEntity(entity.EntityType, entity.Id))
            {
                Log.Warning($"Can't found {entity.EntityType} with ID {entity.Id}");
                return;
            }
            
            Session.SendPacket($"u_i {(int)entity.EntityType} {entity.Id} {(int)item.InventoryType} {item.Slot} 0 0 ");
            Log.Information($"Use item {item.Stack.Item.Id} from inventory {item.InventoryType} in slot {item.Slot} on {entity.EntityType} with ID {entity.Id}");
        }

        /// <summary>
        /// Attack selected entity with selected skill
        /// </summary>
        /// <param name="entity">Entity who is targeted by your skill</param>
        /// <param name="skill">Skill used</param>
        public void Attack(LivingEntity entity, Skill skill)
        {
            if (!Skills.Contains(skill))
            {
                Log.Warning("Trying to use a skill not present in character skills");
                return;
            }

            if (CantAttack)
            {
                return;
            }

            if (skill.IsOnCooldown())
            {
                Log.Warning("Attack on cooldown");
                return;
            }

            if (skill.MpCost > Mp)
            {
                Log.Warning("Mp cost to high");
                return;
            }
            
            switch (skill.Target)
            {
                case SkillTarget.Self:
                    if (!entity.Equals(this))
                    {
                        Log.Warning("Trying to use a self skill on a target");
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
                    Attack(skill, entity.Position);
                    return;
            }
            
            if (!this.IsInSkillRange(entity.Position, skill) && !entity.Equals(this))
            {
                Log.Warning($"Trying to attack entity at {entity.Position} from {Position} but it's out of range");
                return;
            }
            
            skill.LastUse = DateTime.Now.AddMilliseconds(skill.CastTime * 100);

            Session.SendPacket($"u_s {skill.CastId} {(int)entity.EntityType} {entity.Id}");
            Log.Information($"Attacking {entity.Name} with skill {skill.Name}");
        }

        /// <summary>
        /// Attack at defined position (used by skill without target)
        /// </summary>
        /// <param name="skill">Skill to cast</param>
        /// <param name="position">Position where you want to hit</param>
        public void Attack(Skill skill, Position position)
        {
            if (!Skills.Contains(skill))
            {
                Log.Warning("Trying to use a skill not present in character skills");
                return;
            }

            if (CantAttack)
            {
                return;
            }
            
            if (skill.IsOnCooldown())
            {
                return;
            }

            if (skill.MpCost > Mp)
            {
                return;
            }

            if (skill.Target != SkillTarget.NoTarget)
            {
                Log.Warning($"Trying to use a skill at defined position when target should be {skill.Target}");
                return;
            }
            
            if (!this.IsInSkillRange(position, skill))
            {
                Log.Warning($"Trying to attack at {position} from {Position} but it's out of range");
                return;
            }
            
            skill.LastUse = DateTime.Now.AddMilliseconds(skill.CastTime * 100);
            
            Session.SendPacket($"u_as {skill.CastId} {position.X} {position.Y}");
            Log.Debug($"Use skill {skill.CastId} at {position}");
        }

        public void Dispose()
        {
            dispose = true;
            thread.Join();
        }
    }
}