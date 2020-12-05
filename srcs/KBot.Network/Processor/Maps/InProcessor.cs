using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Factory;
using KBot.Game.Maps;
using KBot.Networking.Packet.Maps;

namespace KBot.Networking.Processor.Maps
{
    public class InProcessor : PacketProcessor<In>
    {
        private readonly EntityFactory entityFactory;

        public InProcessor(EntityFactory entityFactory)
        {
            this.entityFactory = entityFactory;
        }

        protected override void Process(GameSession session, In packet)
        {
            Map map = session.Character.Map;

            switch (packet.EntityType)
            {
                case EntityType.Monster:
                    Monster monster = entityFactory.CreateMonster(packet.ModelId);

                    monster.Id = packet.EntityId;
                    monster.Position = packet.Position;
                    monster.HpPercentage = packet.Npc.HpPercentage;
                    monster.MpPercentage = packet.Npc.MpPercentage;
                    monster.Map = map;
                    monster.Map.Monsters[monster.Id] = monster;
                    break;
                
                case EntityType.Npc:
                    Npc npc = entityFactory.CreateNpc(packet.ModelId);

                    npc.Id = packet.EntityId;
                    npc.Name = packet.Npc.Name;
                    npc.Position = packet.Position;
                    npc.HpPercentage = packet.Npc.HpPercentage;
                    npc.MpPercentage = packet.Npc.MpPercentage;
                    npc.Map = map;
                    npc.Map.Npcs[npc.Id] = npc;
                    break;
                
                case EntityType.MapObject:
                    MapObject mapObject = entityFactory.CreateMapObject(packet.ModelId);
                    
                    mapObject.Id = packet.EntityId;
                    mapObject.Position = packet.Position;
                    mapObject.Map = map;
                    mapObject.Map.MapObjects[mapObject.Id] = mapObject;
                    break;
                
                case EntityType.Player:
                    Player player = new Player
                    {
                        Id = packet.EntityId,
                        Gender = packet.Player.Gender,
                        Job = packet.Player.Job,
                        HpPercentage = packet.Player.HpPercentage,
                        MpPercentage = packet.Player.MpPercentage,
                        Position = packet.Position
                    };

                    player.Map = map;
                    player.Map.Players[player.Id] = player;
                    break;
                default:
                    return;
            }
            
            Log.Debug($"Entity {packet.EntityType} with ID {packet.EntityId} added to map.");
        }
    }
}