using System;
using KBot.Game.Enum;

namespace KBot.Game.Battle
{
    public class Buff : IEquatable<Buff>
    {
        public int Id { get; }
        public string Name { get; }
        public int Level { get; }
        public int GroupId { get; }
        public BuffCategory Category { get; }
        public BuffEffect Effect { get; }
        public DateTime ReceiveTime { get; }
        public int Duration { get; }
        public TimeSpan TimeLeft => ReceiveTime.AddSeconds(Duration) - DateTime.Now;
        
        public Buff(int buffId, string name, int groupId, BuffCategory category, BuffEffect effect, int level, int duration)
        {
            Id = buffId;
            Name = name;
            GroupId = groupId;
            Category = category;
            Effect = effect;
            Level = level;
            ReceiveTime = DateTime.Now;
        }

        public bool Equals(Buff other)
        {
            return other != null && other.Id == Id;
        }
    }
}