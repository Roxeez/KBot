using System;
using KBot.Game;
using KBot.Game.Entities;
using PropertyChanged;

namespace KBot.UI.Context
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowContext
    {
        public GameSession Session { get; }
        public Character Character { get; }

        public MainWindowContext(GameSession session)
        {
            Session = session;
            Character = session.Character;
        }
    }
}