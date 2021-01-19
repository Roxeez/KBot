using System.Threading;
using KBot.Event;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;

namespace KBot.Core.Event.Processor
{
    public class UseAmuletOfReturnProcessor : EventProcessor<UseAmuletOfReturnEvent>
    {
        protected override void Process(GameSession session, UseAmuletOfReturnEvent e)
        {
            Character character = session.Character;
            Inventory inventory = character.GetInventory(InventoryType.Etc);

            InventoryItem item = inventory.Find(2071);
            if (item == null)
            {
                return;
            }
            
            Thread.Sleep(1000);
            session.SendPacket("guri 2");
            Thread.Sleep(5000);
            session.SendPacket($"#u_i^1^{character.Id}^{(int)item.InventoryType}^{item.Slot}^2");
            Thread.Sleep(2000);
        }
    }
}