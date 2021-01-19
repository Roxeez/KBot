using System;
using System.Threading;
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
        
        private static readonly NetworkBridge Bridge = new NetworkBridge();

        private readonly NetworkCallback sendCallback;
        private readonly NetworkCallback recvCallback;

        public GameSession()
        {
            Id = Guid.NewGuid();
            Character = new Character(this);
           
            sendCallback = OnPacketSend;
            recvCallback = OnPacketReceived;

            Bridge.AddSendCallback(sendCallback);
            Bridge.AddRecvCallback(recvCallback);
        }

        public void SendPacket(string message)
        {
            Bridge.SendPacket(message);
        }

        public void ReceivePacket(string packet)
        {
            Bridge.SendPacket(packet);
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
            Character.Dispose();
            Bridge.Dispose();
        }
    }
}