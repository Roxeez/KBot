using System;
using KBot.Game.Enum;

namespace KBot.Game.Skills
{
    public class Skill : IEquatable<Skill>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Range { get; set; }
        public short ZoneRange { get; set; }
        public int CastTime { get; set; }
        public int Cooldown { get; set; }
        public SkillCategory Category { get; set; }
        public int MpCost { get; set; }
        public int CastId { get; set; }
        public SkillTarget Target { get; set; }
        public HitType HitType { get; set; }
        public SkillType Type { get; set; }
        
        public bool IsOnCooldown { get; set; }
        
        public bool Equals(Skill other)
        {
            return other != null && other.Id == Id;
        }
    }
}