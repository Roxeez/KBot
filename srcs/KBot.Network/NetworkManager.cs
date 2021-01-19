using System;
using System.Collections.Generic;
using System.Linq;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Network.Packet;
using KBot.Network.Processor;

namespace KBot.Network
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

            try
            {
                processor.Process(session, packet);
            }
            catch (Exception e)
            {
                Log.Error($"Error when processing packet {packet.GetType().Name}", e);
            }
        }
    }
}