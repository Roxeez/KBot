using System.Collections.Generic;
using System.Collections.ObjectModel;
using KBot.Game.Entities;
using KBot.Game.Skills;

namespace KBot.Game.Group
{
    public class Partner
    {
        public LivingEntity Entity { get; }
        public ObservableCollection<Skill> Skills { get; }

        public Partner(LivingEntity entity)
        {
            Entity = entity;
            Skills = new ObservableCollection<Skill>();
        }

        public void Walk(Position position)
        {
            if (!Entity.Map.IsWalkable(position))
            {
                return;
            }
        }
    }
}