namespace KBot.Network.Packet.Pets
{
    public class PClear : IPacket
    {
        
    }

    public class PClearCreator : IPacketCreator
    {
        public string Header { get; } = "p_clear";
        
        public IPacket Create(string[] content)
        {
            return new PClear();
        }
    }
}