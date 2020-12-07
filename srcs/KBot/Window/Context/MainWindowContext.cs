using System;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;
using KBot.Game.Maps;
using KBot.Game.Pets;
using KBot.Game.Battle;
using PropertyChanged;

namespace KBot.Window.Context
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowContext
    {
        public GameSession Session { get; }
        public Character Character { get; }

        public Inventory Inventory { get; set; }

        public ICommand MoveToMonster { get; }
        public ICommand MovePetsToMonster { get; }

        public MainWindowContext(GameSession session)
        {
            Session = session;
            Character = session.Character;

            MoveToMonster = new RelayCommand(OnMoveToMonster);
            MovePetsToMonster = new RelayCommand(OnMovePetsToMonster);
        }

        private void OnMoveToMonster()
        {
            Monster monster = Character.GetClosestMonster();

            if (monster == null)
            {
                return;
            }

            Character.WalkTo(monster);
        }

        private void OnMovePetsToMonster()
        {
            Monster monster = Character.GetClosestMonster();

            if (monster == null)
            {
                return;
            }

            if (Character.Pet != null)
            {
                return;
            }

            Character.Pet.WalkTo(monster);
        }
    }
}