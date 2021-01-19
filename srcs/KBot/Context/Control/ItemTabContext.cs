using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using KBot.Core;
using KBot.Core.Configuration;
using Microsoft.Expression.Interactivity.Core;

namespace KBot.Context.Control
{
    public class ItemTabContext : ITabContext
    {
        public TabContextKey Key => TabContextKey.Item;

        public Bot Bot { get; }

        public ItemConfiguration SelectedHealItem { get; set; }
        public ItemConfiguration SelectedUsedHealItem { get; set; }

        public ICommand AddItem { get; }
        public ICommand RemoveItem { get; }

        public ItemTabContext(Bot bot)
        {
            Bot = bot;
            
            AddItem = new RelayCommand(OnAddItem);
            RemoveItem = new ActionCommand(OnRemoveItem);
        }

        private void OnAddItem()
        {
            if (SelectedHealItem == null)
            {
                return;
            }

            if (!Bot.HealItems.Contains(SelectedHealItem))
            {
                return;
            }

            if (Bot.UsedHealItems.Contains(SelectedHealItem))
            {
                return;
            }

            Bot.UsedHealItems.Add(SelectedHealItem);
        }

        private void OnRemoveItem()
        {
            if (SelectedUsedHealItem == null)
            {
                return;
            }

            if (!Bot.UsedHealItems.Contains(SelectedUsedHealItem))
            {
                return;
            }

            Bot.UsedHealItems.Remove(SelectedHealItem);
        }
    }
}
