using KBot.Common.Extension;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Player
{
    public class CInfo : IPacket
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public Gender Gender { get; set; }
        public Job Job { get; set; }
    }

    public class CInfoCreator : IPacketCreator
    {
        public string Header { get; } = "c_info";
        
        public IPacket Create(string[] content)
        {
            return new CInfo
            {
                Name = content[0],
                Id = content[5].ToLong(),
                Gender = content[7].ToEnum<Gender>(),
                Job = content[10].ToEnum<Job>()
            };
        }
    }
}