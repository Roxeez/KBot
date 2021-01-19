
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using KBot.Common.Extension;
using KBot.Core;
using KBot.Core.Configuration;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Network.Packet.Characters;
using PropertyChanged;

namespace KBot.Context.Control
{
    [AddINotifyPropertyChangedInterface]
    public class GeneralTabContext : ITabContext
    {
        public GameSession Session { get; }
        public Character Character { get; }
        
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        public ICommand SaveProfile { get; }
        public ICommand LoadProfile { get; }
        
        public ObservableCollection<string> Profiles { get; }
        public string SelectedProfile { get; set; }
        
        public Bot Bot { get; }
        
        public TabContextKey Key => TabContextKey.General;

        private readonly ProfileManager profileManager;
        private static readonly Pen Pen = new Pen(Color.DarkBlue, 0.1f);

        public GeneralTabContext(GameSession session, Bot bot, ProfileManager profileManager)
        {
            Session = session;
            Character = session.Character;
            Bot = bot;
            StartCommand = new RelayCommand(OnStartBot);
            StopCommand = new RelayCommand(OnStopBot);
            SaveProfile = new RelayCommand(OnSaveProfile);
            LoadProfile = new RelayCommand(OnLoadProfile);
            
            Profiles = new ObservableCollection<string>(profileManager.GetProfiles());

            this.profileManager = profileManager;
        }

        private void OnLoadProfile()
        {
            if (string.IsNullOrEmpty(SelectedProfile))
            {
                return;
            }

            if (!Profiles.Contains(SelectedProfile))
            {
                return;
            }

            Profile profile = profileManager.LoadProfile(SelectedProfile);
            if (profile == null)
            {
                return;
            }
            
            if (profile.MapId == Session.Character.Map.Id)
            {
                Bot.Path.Clear();
                Bot.Path.AddRange(profile.Path);
                
                using (var graphics = Graphics.FromImage(Session.Character.Map.Preview))
                {
                    var previous = new Position(0, 0);
                    for (int i = 0; i < Bot.Path.Count; i++)
                    {
                        Position position = Bot.Path[i];

                        if (i != 0)
                        {
                            graphics.DrawLine(Pen, previous.X, previous.Y, position.X, position.Y);
                        }
                        graphics.FillRectangle(Brushes.DarkRed, position.X, position.Y, 1, 1);
                        previous = position;
                    }
                }

                Bot.MapPreview = Session.Character.Map.Preview.ToBitmapSource();
            }

            IEnumerable<Monster> monsters = Bot.Monsters.Where(x => profile.Monsters.Any(s => s == x.ModelId));
            
            Bot.WhitelistedMonsters.Clear();
            Bot.WhitelistedMonsters.AddRange(monsters);

            IEnumerable<Skill> buffs = Bot.BuffSkills.Where(x => profile.BuffSkills.Any(s => s == x.Id));
            IEnumerable<Skill> skills = Bot.DamageSkills.Where(x => profile.DamageSkills.Any(s => s == x.Id));
            
            Bot.UsedDamageSkills.Clear();
            Bot.UsedBuffSkills.Clear();
            
            Bot.UsedDamageSkills.AddRange(skills);
            Bot.UsedBuffSkills.AddRange(buffs);

            IEnumerable<ItemConfiguration> healItems = Bot.HealItems.Where(x => profile.HealItems.Any(s => s.ItemId == x.Item.Id));
            foreach (ItemConfiguration configuration in healItems)
            {
                HealItem saved = profile.HealItems.First(x => x.ItemId == configuration.Item.Id);
                
                configuration.UseForHp = saved.UseForHp;
                configuration.UseForMp = saved.UseForMp;
                configuration.HpPercentage = saved.HpPercentage;
                configuration.MpPercentage = saved.MpPercentage;
            }
            
            Bot.UsedHealItems.Clear();
            Bot.UsedHealItems.AddRange(healItems);

            Bot.PickUpGolds = profile.PickUpGolds;
            Bot.UseAmuletOfReturn = profile.UseAmuletOfReturn;
            Bot.PickUpSoundFlowers = profile.PickUpSoundFlowers;
            Bot.UseAncelloanBlessing = profile.UseAncelloanBlessing;
            Bot.UseAttackPotion = profile.UseAttackPotion;
            Bot.UseDefencePotion = profile.UseDefencePotion;
            Bot.UseEnergyPotion = profile.UseEnergyPotion;
            Bot.UseExperiencePotion = profile.UseExperiencePotion;
            Bot.UseFairyBoost = profile.UseFairyBoost;
            Bot.UseMateBlessing = profile.UseMateBlessing;
            Bot.UsePetFood = profile.UsePetFood;
            Bot.UseSoulstoneBlessing = profile.UseSoulstoneBlessing;
        }

        private void OnSaveProfile()
        {
            if (Session.Character.Map == null)
            {
                return;
            }
            
            var profile = new Profile
            {
                MapId = Session.Character.Map.Id,
                BuffSkills = Bot.UsedBuffSkills.Select(x => x.Id).ToList(),
                DamageSkills = Bot.UsedDamageSkills.Select(x => x.Id).ToList(),
                HealItems = Bot.UsedHealItems.Select(x => new HealItem
                {
                    ItemId = x.Item.Id,
                    HpPercentage = x.HpPercentage,
                    MpPercentage = x.MpPercentage,
                    UseForHp = x.UseForHp,
                    UseForMp = x.UseForMp
                }).ToList(),
                Monsters = Bot.WhitelistedMonsters.Select(x => x.ModelId).ToList(),
                Path = Bot.Path.ToList(),
                PickUpGolds = Bot.PickUpGolds,
                UseAmuletOfReturn = Bot.UseAmuletOfReturn,
                PickUpSoundFlowers = Bot.PickUpSoundFlowers,
                UseAncelloanBlessing = Bot.UseAncelloanBlessing,
                UseAttackPotion = Bot.UseAttackPotion,
                UseDefencePotion = Bot.UseDefencePotion,
                UseEnergyPotion = Bot.UseEnergyPotion,
                UseExperiencePotion = Bot.UseExperiencePotion,
                UseFairyBoost = Bot.UseFairyBoost,
                UseMateBlessing = Bot.UseMateBlessing,
                UsePetFood = Bot.UsePetFood,
                UseSoulstoneBlessing = Bot.UseSoulstoneBlessing
            };

            string name = $"{Session.Character.Name} ({Session.Character.Map.Name})";
            if (SelectedProfile != null)
            {
                if (name != SelectedProfile)
                {
                    int id = 0;
                    while (Profiles.Contains(name))
                    {
                        name += $" - {++id}";
                    }
                    
                    Profiles.Add(name);
                }
            }
            
            profileManager.SaveProfile(name, profile);
        }

        private void OnStartBot()
        {
            Bot.Start();
        }

        private void OnStopBot()
        {
            Bot.Stop();
        }
    }
}
