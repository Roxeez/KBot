using System;
using System.Collections.Generic;
using System.Linq;
using KBot.Extension;

namespace KBot.Networking.Packet
{
    public sealed class PacketFactory
    {
        private readonly Dictionary<string, IPacketCreator> creators;

        public PacketFactory(IEnumerable<IPacketCreator> creators)
        {
            this.creators = creators.ToDictionary(x => x.Header, x => x);
        }
        
        public IPacket CreateTypedPacket(string packet)
        {
            string[] split = packet.Split(' ');
            if (split.Length == 0)
            {
                throw new InvalidOperationException("Incorrect packet length");
            }

            string header = split[0];
            string[] content = split.Length > 1 ? split.Skip(1).ToArray() : Array.Empty<string>();

            IPacketCreator creator = creators.GetValue(header);
            if (creator == null)
            {
                return new UndefinedPacket
                {
                    Header = header,
                    Content = content,
                    Packet = packet
                };
            }

            return creator.Create(content);
        }
    }
}