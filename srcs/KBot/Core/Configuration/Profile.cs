using System.Collections.Generic;
using KBot.Game;

namespace KBot.Core.Configuration
{
    public class Profile
    {
        public int MapId { get; set; }
        
        public List<int> DamageSkills { get; set; }
        public List<int> BuffSkills { get; set; }
        public List<HealItem> HealItems { get; set; }
        public List<int> Monsters { get; set; }
        public List<Position> Path { get; set; }
        
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
    }

    public class HealItem
    {
        public int ItemId { get; set; }
        public bool UseForHp { get; set; }
        public bool UseForMp { get; set; }
        public int HpPercentage { get; set; }
        public int MpPercentage { get; set; }
    }
}