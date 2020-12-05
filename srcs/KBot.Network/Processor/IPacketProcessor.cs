using System;
using KBot.Game;
using KBot.Network.Packet;

namespace KBot.Network.Processor
{
    public interface IPacketProcessor
    {
        Type PacketType { get; }

        void Process(GameSession session, IPacket packet);
    }

    public abstract class PacketProcessor<T> : IPacketProcessor where T : IPacket
    {
        public Type PacketType { get; } = typeof(T);
        
        public void Process(GameSession session, IPacket packet)
        {
            Process(session, (T)packet);
        }

        protected abstract void Process(GameSession session, T packet);
    }
}