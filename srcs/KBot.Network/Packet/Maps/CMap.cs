using KBot.Extension;

namespace KBot.Networking.Packet.Maps
{
    public class CMap : IPacket
    {
        public int MapId { get; set; }
        public bool IsBaseMap { get; set; }
    }

    public class CMapCreator : IPacketCreator
    {
        public string Header { get; } = "c_map";
        
        public IPacket Create(string[] content)
        {
            return new CMap
            {
                MapId = content[1].ToInt(),
                IsBaseMap = content[2].ToBool()
            };
        }
    }
}