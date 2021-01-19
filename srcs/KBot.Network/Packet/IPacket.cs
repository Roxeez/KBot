namespace KBot.Network.Packet
{
    public interface IPacket
    {
        
    }
    
    public interface IPacketCreator
    {
        string Header { get; }
        PacketType PacketType { get; }
        
        IPacket Create(string[] content);
    }
}