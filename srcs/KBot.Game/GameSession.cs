using System;
using KBot.Game.Entities;
using KBot.Interop;

namespace KBot.Game
{
    public sealed class GameSession : IDisposable
    {
        public Guid Id { get; }
        public Character Character { get; }

        public event Action<string> PacketSend;
        public event Action<string> PacketReceived;
        
        private readonly Network network;
        private readonly NetworkCallback sendCallback;
        private readonly NetworkCallback recvCallback;

        public GameSession()
        {
            Id = Guid.NewGuid();
            Character = new Character(this);
            
            network = new Network();

            sendCallback = OnPacketSend;
            recvCallback = OnPacketReceived;
            
            network.AddSendCallback(sendCallback);
            network.AddRecvCallback(recvCallback);
        }

        public void SendPacket(string message)
        {
            network.SendPacket(message);
        }

        public void ReceivePacket(string packet)
        {
            network.SendPacket(packet);
        }

        private void OnPacketSend(string packet)
        {
            PacketSend?.Invoke(packet);
        }

        private void OnPacketReceived(string packet)
        {
            PacketReceived?.Invoke(packet);
        }

        public void Dispose()
        {
            network.Dispose();
        }
    }
}