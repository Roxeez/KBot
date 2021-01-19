namespace KBot.Data
{
    public class SkillData
    {
        public string NameKey { get; set; }
        public short Range { get; set; }
        public short ZoneRange { get; set; }
        public int CastTime { get; set; }
        public int Cooldown { get; set; }
        public int Category { get; set; }
        public int MpCost { get; set; }
        public int CastId { get; set; }
        public int Target { get; set; }
        public int HitType { get; set; }
        public int Type { get; set; }
        public bool IsCombo { get; set; }
        public int Icon { get; set; }
    }
}