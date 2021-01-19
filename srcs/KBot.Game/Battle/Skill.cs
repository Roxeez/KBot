using System;
using System.Collections;
using System.Drawing;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Windows.Media.Imaging;
using KBot.Game.Enum;

namespace KBot.Game.Battle
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
        public bool IsCombo { get; set; }
        public DateTime LastUse { get; set; }
        public BitmapImage Icon { get; set; }
        
        public bool Equals(Skill other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Skill)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}