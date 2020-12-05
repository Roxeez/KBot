using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Maps;

namespace KBot.Game.Extension
{
    public static class MapExtensions
    {
        public static T GetEntity<T>(this Map map, EntityType type, long entityId) where T : Entity
        {
            return map.GetEntity(type, entityId) as T;
        }

        public static void RemoveEntity(this Map map, Entity entity)
        {
            switch (entity.EntityType)
            {
                case EntityType.Monster:
                    map.Monsters.Remove(entity.Id);
                    break;
                case EntityType.Npc:
                    map.Npcs.Remove(entity.Id);
                    break;
                case EntityType.Player:
                    map.Players.Remove(entity.Id);
                    break;
                case EntityType.MapObject:
                    map.MapObjects.Remove(entity.Id);
                    break;
                default:
                    return;
            }
        }
    }
}