namespace KBot.Network.Packet
{
    public interface IPacket
    {
        
    }
    
    public interface IPacketCreator
    {
        string Header { get; }
        IPacket Create(string[] content);
    }
}