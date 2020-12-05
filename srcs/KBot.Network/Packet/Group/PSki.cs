using System;
using System.Linq;

namespace KBot.Network.Packet.Group
{
    public class PSki : IPacket
    {
        public int[] Skills { get; set; }
    }

    public class PSkiCreator : IPacketCreator
    {
        public string Header { get; } = "pski";
        
        public IPacket Create(string[] content)
        {
            return new PSki
            {
                Skills = content.Select(x => Convert.ToInt32(x)).ToArray()
            };
        }
    }
}