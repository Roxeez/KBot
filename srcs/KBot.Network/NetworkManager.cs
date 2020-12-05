using System;
using System.Collections.Generic;
using System.Linq;
using KBot.Extension;
using KBot.Game;
using KBot.Networking.Packet;
using KBot.Networking.Processor;

namespace KBot.Networking
{
    public class NetworkManager
    {
        private readonly Dictionary<Type, IPacketProcessor> processors;

        public NetworkManager(IEnumerable<IPacketProcessor> processors)
        {
            this.processors = processors.ToDictionary(x => x.PacketType, x => x);
        }
        
        public void Process(GameSession session, IPacket packet)
        {
            IPacketProcessor processor = processors.GetValue(packet.GetType());
            if (processor == null)
            {
                return;
            }
            
            processor.Process(session, packet);
        }
    }
}