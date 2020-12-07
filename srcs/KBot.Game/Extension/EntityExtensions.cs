using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBot.Game.Entities;

namespace KBot.Game.Extension
{
    public static class EntityExtensions
    {
        public static Monster GetClosestMonster(this Entity entity)
        {
            return entity.Map.Monsters.Values.OrderBy(x => x.Position.GetDistance(entity.Position)).FirstOrDefault();
        }
    }
}
