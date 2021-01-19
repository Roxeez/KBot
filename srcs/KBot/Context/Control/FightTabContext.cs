using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using KBot.Core;
using KBot.Game.Battle;
using KBot.Game.Entities;
using PropertyChanged;

namespace KBot.Context.Control
{
    [AddINotifyPropertyChangedInterface]
    public class FightTabContext : ITabContext
    {
        public TabContextKey Key => TabContextKey.Fight;
        
        public Bot Bot { get; }
        public Character Character { get; }
        
        
        public Skill Skill { get; set; }
        public Skill UsedSkill { get; set; }
        public ICommand AddSkill { get; }
        public ICommand RemoveSkill { get; }
        
        public Monster Monster { get; set; }
        public Monster ListedMonster { get; set; }
        public ICommand AddMonster { get; }
        public ICommand RemoveMonster { get; }
        
        public Skill Buff { get; set; }
        public Skill UsedBuff { get; set; }
        public ICommand AddBuff { get;  }
        public ICommand RemoveBuff { get;  }

        public FightTabContext(Bot bot)
        {
            Bot = bot;
            Character = bot.Session.Character;

            AddSkill = new RelayCommand(OnAddSkill);
            RemoveSkill = new RelayCommand(OnRemoveSkill);
            
            AddMonster = new RelayCommand(OnAddMonster);
            RemoveMonster = new RelayCommand(OnRemoveMonster);
            
            AddBuff = new RelayCommand(OnAddBuff);
            RemoveBuff = new RelayCommand(OnRemoveBuff);
        }
        
        private void OnAddSkill()
        {
            if (Skill == null)
            {
                return;
            }

            if (!Bot.DamageSkills.Contains(Skill))
            {
                return;
            }

            if (Bot.UsedDamageSkills.Contains(Skill))
            {
                return;
            }

            Bot.UsedDamageSkills.Add(Skill);
        }

        private void OnRemoveSkill()
        {
            if (UsedSkill == null)
            {
                return;
            }

            Bot.UsedDamageSkills.Remove(UsedSkill);
        }

        private void OnAddBuff()
        {
            if (Buff == null)
            {
                return;
            }
            
            if (!Bot.BuffSkills.Contains(Buff))
            {
                return;
            }

            if (Bot.UsedBuffSkills.Contains(Buff))
            {
                return;
            }

            Bot.UsedBuffSkills.Add(Buff);
        }

        private void OnRemoveBuff()
        {
            if (UsedBuff == null)
            {
                return;
            }

            Bot.UsedBuffSkills.Remove(UsedBuff);
        }

        private void OnAddMonster()
        {
            if (Monster == null)
            {
                return;
            }

            if (!Bot.Monsters.Contains(Monster))
            {
                return;
            }

            if (Bot.WhitelistedMonsters.Contains(Monster))
            {
                return;
            }

            Bot.WhitelistedMonsters.Add(Monster);
        }

        private void OnRemoveMonster()
        {
            if (ListedMonster == null)
            {
                return;
            }

            if (!Bot.WhitelistedMonsters.Contains(ListedMonster))
            {
                return;
            }

            Bot.WhitelistedMonsters.Remove(ListedMonster);
        }
    }
}
