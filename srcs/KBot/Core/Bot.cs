using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Media.Imaging;
using KBot.Collection;
using KBot.Common.Logging;
using KBot.Core.Configuration;
using KBot.Core.Event;
using KBot.Event;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Inventories;
using KBot.Game.Pets;
using KBot.Network.Packet.Characters;
using PropertyChanged;

namespace KBot.Core
{
    [AddINotifyPropertyChangedInterface]
    public sealed class Bot
    {
        public GameSession Session { get; }
        
        public ObservableConcurrentCollection<Monster> Monsters { get; }
        public ObservableConcurrentCollection<Monster> WhitelistedMonsters { get; }
        
        public ObservableConcurrentCollection<Skill> DamageSkills { get; }
        public ObservableConcurrentCollection<Skill> BuffSkills { get; }
        public ObservableConcurrentCollection<Skill> UsedDamageSkills { get; }
        public ObservableConcurrentCollection<Skill> UsedBuffSkills { get; }

        public ObservableConcurrentCollection<ItemConfiguration> HealItems { get; }
        public ObservableConcurrentCollection<ItemConfiguration> UsedHealItems { get; }

        public ObservableConcurrentCollection<Position> Path { get; }
        
        public bool UseAncelloanBlessing { get; set; }
        public bool UseMateBlessing { get; set; }        
        public bool UseFairyBoost { get; set; }
        public bool UseSoulstoneBlessing { get; set; }
        
        public bool UseAttackPotion { get; set; }
        public bool UseDefencePotion { get; set; }
        public bool UseEnergyPotion { get; set; }
        public bool UseExperiencePotion { get; set; }

        public bool PickUpSoundFlowers { get; set; }
        public bool PickUpGolds { get; set; }
        public bool UsePetFood { get; set; }

        public bool UseAmuletOfReturn { get; set; }
        
        public bool IsRecordingPath { get; set; }
        
        public BitmapImage MapPreview { get; set; }

        private Thread thread;
        private readonly EventPipeline eventPipeline;
        
        public bool IsRunning { get; set; }
        
        public Bot(GameSession session, EventPipeline eventPipeline)
        {
            Session = session;
            this.eventPipeline = eventPipeline;

            Monsters = new ObservableConcurrentCollection<Monster>();
            WhitelistedMonsters = new ObservableConcurrentCollection<Monster>();
            
            DamageSkills = new ObservableConcurrentCollection<Skill>();
            BuffSkills = new ObservableConcurrentCollection<Skill>();
            
            UsedBuffSkills = new ObservableConcurrentCollection<Skill>();
            UsedDamageSkills = new ObservableConcurrentCollection<Skill>();
            
            HealItems = new ObservableConcurrentCollection<ItemConfiguration>();
            UsedHealItems = new ObservableConcurrentCollection<ItemConfiguration>();
            
            Path = new ObservableConcurrentCollection<Position>();
        }

        public void Start()
        {
            thread = new Thread(Loop);
            thread.Start();
        }

        private void Loop()
        {
            IsRecordingPath = false;
            IsRunning = true;

            int waypointIndex = 0;
            while (IsRunning)
            {
                Position waypoint = Path.Count == 0 ? Session.Character.Position : Path[waypointIndex];
                
                eventPipeline.Process(Session, new WalkToWaypointEvent(waypoint));
                eventPipeline.Process(Session, new UseBuffsEvent());
                eventPipeline.Process(Session, new UseConsumablesEvent());
                eventPipeline.Process(Session, new KillMonstersEvent(waypoint));
                eventPipeline.Process(Session, new PickUpDropsEvent(waypoint));

                waypointIndex = waypointIndex == (Path.Count - 1) ? 0 : waypointIndex + 1;

                if (UseAmuletOfReturn && waypointIndex == 0 && Path.Count > 0)
                {
                    eventPipeline.Process(Session, new UseAmuletOfReturnEvent());
                }
            }
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}