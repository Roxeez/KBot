using KBot.Game.Entities;
using KBot.Game.Enum;

namespace KBot.Game.Extension
{
    public static class CharacterExtensions
    {
        public static void WalkInRange(this Character character, Position position, int range)
        {
            double distance = character.Position.GetDistance(position);
            if (distance <= range)
            {
                return;
            }

            double ratio = (distance - range) / distance;

            double x = character.Position.X + (ratio * (position.X - character.Position.X));
            double y = character.Position.Y + (ratio * (position.Y - character.Position.Y));

            character.Walk(new Position((short)x, (short)y));
        }
    }
}