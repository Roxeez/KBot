using KBot.Game.Entities;
using KBot.Game.Pets;

namespace KBot.Game.Extension
{
    public static class PartnerExtensions
    {
        public static void WalkTo(this Partner partner, Entity entity)
        {
            partner.Walk(entity.Position);
        }
    }
}