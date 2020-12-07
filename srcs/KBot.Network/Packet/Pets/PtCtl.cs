using System.Collections.Generic;
using System.Linq;
using KBot.Common.Extension;
using KBot.Game;
using KBot.Game.Enum;

namespace KBot.Network.Packet.Pets
{
    public class PtCtl : IPacket
    {
        public PtCtlSub[] Pets { get; set; }
        public int Speed { get; set; }
    }

    public class PtCtlSub
    {
        public long EntityId { get; set; }
        public Position Position { get; set; }
    }

    public class PtCtlCreator : IPacketCreator
    {
        public string Header { get; } = "ptctl";
        
        public IPacket Create(string[] content)
        {
            int count = content[1].ToInt();

            var pets = new PtCtlSub[count];
            for (int i = 0; i < count; i++)
            {
                int index = (i * 3) + 2;
                pets[i] = new PtCtlSub
                {
                    EntityId = content[index].ToLong(),
                    Position = new Position(content[index + 1].ToShort(), content[index + 2].ToShort())
                };
            }

            return new PtCtl
            {
                Pets = pets,
                Speed = content.Last().ToInt()
            };
        }
    }
}