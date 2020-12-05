using System;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Maps;
using KBot.Network.Packet.Maps;

namespace KBot.Network.Processor.Maps
{
    public class CMapProcessor : PacketProcessor<CMap>
    {
        private readonly MapFactory mapFactory;

        public CMapProcessor(MapFactory mapFactory)
        {
            this.mapFactory = mapFactory;
        }
        
        protected override void Process(GameSession session, CMap packet)
        {
            Character character = session.Character;
            
            Map map = mapFactory.CreateMap(packet.MapId);

            character.Map = map;
            character.Map.Players[character.Id] = character;
            
            Log.Debug($"Map changed to {map.Id}");
        }
    }
}