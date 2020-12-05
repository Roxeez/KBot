using System;
using System.Linq;

namespace KBot.Network.Packet.Group
{
    public class PetSki : IPacket
    {
        public int[] Skills { get; set; }
    }

    public class PetSkiCreator : IPacketCreator
    {
        public string Header { get; } = "petski";
        
        public IPacket Create(string[] content)
        {
            return new PSki
            {
                Skills = content.Select(x => Convert.ToInt32(x)).ToArray()
            };
        }
    }
}