using System.Linq;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Game.Pets;
using KBot.Network.Packet.Maps;

namespace KBot.Network.Processor.Maps
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
                    Monster monster = entityFactory.CreateMonster(packet.ModelId, packet.EntityId);
                    
                    monster.Position = packet.Position;
                    monster.HpPercentage = packet.Npc.HpPercentage;
                    monster.MpPercentage = packet.Npc.MpPercentage;
                    monster.Map = map;
                    
                    monster.Map.Monsters[monster.Id] = monster;
                    break;
                
                case EntityType.Npc:
                    Npc npc = entityFactory.CreateNpc(packet.ModelId, packet.EntityId, packet.Npc.Name);
                    
                    npc.Position = packet.Position;
                    npc.HpPercentage = packet.Npc.HpPercentage;
                    npc.MpPercentage = packet.Npc.MpPercentage;
                    npc.Map = map;

                    npc.Map.Npcs[npc.Id] = npc;
                    break;
                
                case EntityType.MapObject:
                    MapObject mapObject = entityFactory.CreateMapObject(packet.ModelId, packet.EntityId, packet.MapObject.Amount);
                    Player owner = map.GetEntity<Player>(EntityType.Player, packet.MapObject.Owner);
                    if (owner == null)
                    {
                        Log.Debug($"Can't found owner of map object {mapObject.Id}");
                    }
                    
                    mapObject.Position = packet.Position;
                    mapObject.Owner = owner;
                    mapObject.Map = map;

                    mapObject.Map.MapObjects[mapObject.Id] = mapObject;
                    break;
                
                case EntityType.Player:
                    var player = new Player(packet.EntityId, packet.Name)
                    {
                        Gender = packet.Player.Gender,
                        Job = packet.Player.Job,
                        HpPercentage = packet.Player.HpPercentage,
                        MpPercentage = packet.Player.MpPercentage,
                        Position = packet.Position,
                        Level = packet.Player.Level,
                        HeroLevel = packet.Player.HeroLevel,
                        Map = map
                    };

                    player.Map.Players[player.Id] = player;
                    break;
                default:
                    return;
            }
            
            Log.Debug($"Entity {packet.EntityType} with ID {packet.EntityId} added to map.");
        }
    }
}