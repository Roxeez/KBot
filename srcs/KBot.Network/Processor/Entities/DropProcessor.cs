using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;
using KBot.Game.Maps;
using KBot.Network.Packet;
using KBot.Network.Packet.Entities;

namespace KBot.Network.Processor.Entities
{
    public class DropProcessor : PacketProcessor<Drop>
    {
        private readonly ItemFactory itemFactory;

        public DropProcessor(ItemFactory itemFactory)
        {
            this.itemFactory = itemFactory;
        }
        
        protected override void Process(GameSession session, Drop packet)
        {
            Map map = session.Character.Map;
            Item item = itemFactory.CreateItem(packet.ModelId);
            
            map.MapObjects[packet.DropId] = new MapObject(packet.DropId, new ItemStack(item, packet.Amount))
            {
                Map = map,
                Position = packet.Position,
                Owner = map.GetEntity<Player>(EntityType.Player, packet.OwnerId)
            };
            
            Log.Debug($"Drop with ID {packet.DropId} with model {packet.ModelId} owned by {packet.OwnerId} successfully added to map");
        }
    }
}