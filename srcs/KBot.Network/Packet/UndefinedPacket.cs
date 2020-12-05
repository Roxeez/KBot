namespace KBot.Network.Packet
{
    public class UndefinedPacket : IPacket
    {
        public string Packet { get; set; }
        public string Header { get; set; }
        public string[] Content { get; set; }
    }
}