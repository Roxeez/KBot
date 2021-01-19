using KBot.Game.Inventories;

namespace KBot.Event.Characters
{
    public class InventoryChangeEvent : IEvent
    {
        public Inventory Inventory { get; set; }
    }
}