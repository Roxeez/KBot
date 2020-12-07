using KBot.Game.Entities;
using KBot.Game.Pets;

namespace KBot.Game.Extension
{
    public static class PetExtensions
    {
        public static void WalkTo(this Pet pet, Entity entity)
        {
            pet.Walk(entity.Position);
        }
    }
}