using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using KBot.Common.Extension;
using KBot.Core;
using KBot.Game.Entities;

namespace KBot.Context.Control
{
    public class PathTabContext : ITabContext
    {
        public Bot Bot { get; }
        public Character Character { get; }

        public ICommand ClearPath { get; }

        public PathTabContext(Bot bot)
        {
            Bot = bot;
            Character = bot.Session.Character;

            ClearPath = new RelayCommand(OnClearPath);
        }

        public TabContextKey Key { get; } = TabContextKey.Path;

        private void OnClearPath()
        {
            Bot.Path.Clear();
            Bot.MapPreview = Character.Map.Preview.ToBitmapSource();
        }
    }
}