using System;
using System.Collections.Generic;
using System.Linq;
using KBot.Common.Extension;

namespace KBot.Network.Packet
{
    public sealed class PacketFactory
    {
        private readonly Dictionary<PacketType, Dictionary<string, IPacketCreator>> creators;

        public PacketFactory(IEnumerable<IPacketCreator> creators)
        {
            this.creators = new Dictionary<PacketType, Dictionary<string, IPacketCreator>>();
            foreach (IPacketCreator creator in creators)
            {
                Dictionary<string, IPacketCreator> sidedCreators = this.creators.GetValue(creator.PacketType);
                if (sidedCreators == null)
                {
                    sidedCreators = new Dictionary<string, IPacketCreator>();
                    this.creators[creator.PacketType] = sidedCreators;
                }

                sidedCreators[creator.Header] = creator;
            }
        }
        
        public IPacket CreateTypedPacket(string packet, PacketType type)
        {
            string[] split = packet.Split(' ');
            if (split.Length == 0)
            {
                throw new InvalidOperationException("Incorrect packet length");
            }

            string header = split[0];
            string[] content = split.Length > 1 ? split.Skip(1).ToArray() : Array.Empty<string>();

            IPacketCreator creator = creators.GetValue(type)?.GetValue(header);
            if (creator == null)
            {
                return new UndefinedPacket
                {
                    Header = header,
                    PacketType = type,
                    Content = content,
                    Packet = packet
                };
            }

            return creator.Create(content);
        }
    }
}