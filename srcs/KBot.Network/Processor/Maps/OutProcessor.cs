using System;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Enum;
using KBot.Game.Maps;
using KBot.Networking.Packet.Maps;

namespace KBot.Networking.Processor.Maps
{
    public class OutProcessor : PacketProcessor<Out>
    {
        protected override void Process(GameSession session, Out packet)
        {
            Map map = session.Character.Map;
            
            switch (packet.EntityType)
            {
                case EntityType.Monster:
                    map.Monsters.Remove(packet.EntityId);
                    break;
                case EntityType.Npc:
                    map.Npcs.Remove(packet.EntityId);
                    break;
                case EntityType.Player:
                    map.Players.Remove(packet.EntityId);
                    break;
                case EntityType.MapObject:
                    map.MapObjects.Remove(packet.EntityId);
                    break;
                default:
                    return;
            }
            
            Log.Debug($"Entity {packet.EntityType} with ID {packet.EntityId} removed from map");
        }
    }
}