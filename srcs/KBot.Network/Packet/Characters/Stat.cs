using KBot.Common.Extension;

namespace KBot.Network.Packet.Characters
{
    public class Stat : IPacket
    {
        public int Hp { get; set; }
        public int HpMaximum { get; set; }
        public int Mp { get; set; }
        public int MpMaximum { get; set; }
    }

    public class StatCreator : IPacketCreator
    {
        public string Header { get; } = "stat";
        
        public IPacket Create(string[] content)
        {
            return new Stat
            {
                Hp = content[0].ToInt(),
                HpMaximum = content[1].ToInt(),
                Mp = content[2].ToInt(),
                MpMaximum = content[3].ToInt()
            };
        }
    }
}