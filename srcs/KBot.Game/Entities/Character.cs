using System;
using KBot.Common.Collection;
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
    public sealed class Character : Player
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
        public ObservableSet<Skill> Skills { get; }
        
        /// <summary>
        /// Contains all main inventories of this character (collection can't be null)
        /// </summary>
        public ObservableDictionary<InventoryType, Inventory> Inventories { get; }
        
        /// <summary>
        /// Contains all pets of this character (collection can't be null)
        /// </summary>
        public ObservableSet<OwnedPet> Pets { get; }
        
        /// <summary>
        /// Contains all partners of this character (collection can't be null)
        /// </summary>
        public ObservableSet<OwnedPartner> Partners { get; }
        
        /// <summary>
        /// Current pet following character (can be null if none)
        /// </summary>
        public Pet Pet { get; set; }
        
        /// <summary>
        /// Current partner following character (can be null if none)
        /// </summary>
        public Partner Partner { get; set; }
        
        private readonly CharacterBridge bridge;
        
        public Character(GameSession session) : base(0, string.Empty)
        {
            bridge = new CharacterBridge();
            
            Session = session;
            
            Skills = new ObservableSet<Skill>();
            Inventories = new ObservableDictionary<InventoryType, Inventory>();
            Pets = new ObservableSet<OwnedPet>();
            Partners = new ObservableSet<OwnedPartner>();
        }

        /// <summary>
        /// Move to selected position
        /// </summary>
        /// <param name="position">Position where you want to move</param>
        public void Walk(Position position)
        {
            if (!Map.IsWalkable(position))
            {
                Log.Warning($"Position is not walkable, can't move to {position}");
                return;
            }

            bridge.Walk(position.X, position.Y);
            Log.Debug($"Move to {position}");
        }

        /// <summary>
        /// Use item on yourself
        /// </summary>
        /// <param name="stack">Item you want to use</param>
        public void UseItem(InventoryItem stack)
        {
            UseItemOn(stack, this);
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
            Log.Debug($"Used item {item.Stack.Item.Id} from {item.InventoryType} in slot {item.Slot} on {entity.EntityType} with ID {entity.Id}");
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

            if (!Position.IsInRange(entity.Position, skill.Range + 1))
            {
                Log.Warning($"Trying to attack entity at {entity.Position} from {Position} but it's out of skill range ({skill.Range} cells)");
                return;
            }

            Session.SendPacket($"u_s {skill.CastId} {(int)entity.EntityType} {entity.Id}");
            Log.Debug($"Used skill {skill.CastId} on {entity.EntityType} with ID {entity.Id}");
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
            
            Log.Debug($"Used skill {skill.CastId} at {position}");
        }
    }
}