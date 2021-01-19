using KBot.Common.Logging;
using KBot.Event;
using KBot.Event.Maps;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Maps;
using KBot.Network.Packet.Maps;

namespace KBot.Network.Processor.Maps
{
    public class AtProcessor : PacketProcessor<At>
    {
        private readonly MapFactory mapFactory;
        private readonly EventPipeline eventPipeline;

        public AtProcessor(MapFactory mapFactory, EventPipeline eventPipeline)
        {
            this.mapFactory = mapFactory;
            this.eventPipeline = eventPipeline;
        }
        
        protected override void Process(GameSession session, At packet)
        {
            Character character = session.Character;

            Map source = character.Map;
            Map destination = mapFactory.CreateMap(packet.MapId);

            character.Map = destination;
            character.Map.Players[character.Id] = character;
            // character.Position = packet.Position;
            
            eventPipeline.Process(session, new MapChangeEvent
            {
                Source = source,
                Destination = destination
            });
            
            Log.Information($"Successfully joined map with ID {destination.Id}");
        }
    }
}