using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Maps;
using KBot.Network.Packet.Maps;

namespace KBot.Network.Processor.Maps
{
    public class GpProcessor : PacketProcessor<Gp>
    {
        protected override void Process(GameSession session, Gp packet)
        {
            Map map = session.Character.Map;

            var portal = new Portal
            {
                Id = packet.Id,
                DestinationId = packet.DestinationId,
                Position = packet.Position,
                Type = packet.PortalType,
                Map = map
            };

            map.Portals[portal.Id] = portal;
            Log.Debug($"Portal of type {portal.Type} with ID {portal.Id} at {portal.Position} added to map");
        }
    }
}