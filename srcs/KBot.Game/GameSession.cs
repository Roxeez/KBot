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
        
        private readonly NetworkBridge bridge;
        private readonly NetworkCallback sendCallback;
        private readonly NetworkCallback recvCallback;

        public GameSession()
        {
            Id = Guid.NewGuid();
            Character = new Character(this);
            
            bridge = new NetworkBridge();

            sendCallback = OnPacketSend;
            recvCallback = OnPacketReceived;

            bridge.AddSendCallback(sendCallback);
            bridge.AddRecvCallback(recvCallback);
        }

        public void SendPacket(string message)
        {
            bridge.SendPacket(message);
        }

        public void ReceivePacket(string packet)
        {
            bridge.SendPacket(packet);
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
            bridge.Dispose();
        }
    }
}