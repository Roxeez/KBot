using KBot.Game;
using KBot.Game.Entities;
using KBot.Network.Packet.Pets;

namespace KBot.Network.Processor.Pets
{
    public class PtCtlProcessor : PacketProcessor<PtCtl>
    {
        protected override void Process(GameSession session, PtCtl packet)
        {
            Character character = session.Character;
            
            foreach (PtCtlSub pet in packet.Pets)
            {
                if (character.Pet != null && character.Pet.Id == pet.EntityId)
                {
                    character.Pet.Position = pet.Position;
                    character.Pet.Speed = packet.Speed;
                }

                if (character.Partner != null && character.Partner.Id == pet.EntityId)
                {
                    character.Partner.Position = pet.Position;
                    character.Partner.Speed = packet.Speed;
                }
            }
        }
    }
}