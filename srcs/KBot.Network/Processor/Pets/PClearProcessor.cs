using KBot.Game;
using KBot.Network.Packet.Pets;

namespace KBot.Network.Processor.Pets
{
    public class PClearProcessor : PacketProcessor<PClear>
    {
        protected override void Process(GameSession session, PClear packet)
        {
            session.Character.Pet = null;
            session.Character.Partner = null;
            
            session.Character.Pets.Clear();
        }
    }
}