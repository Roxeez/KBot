using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Group;
using KBot.Network.Packet.Group;

namespace KBot.Network.Processor.Group
{
    public class PtCtlProcessor : PacketProcessor<PtCtl>
    {
        protected override void Process(GameSession session, PtCtl packet)
        {
            Character character = session.Character;
            Partner partner = character.Partner;
            Pet pet = character.Pet;
            
            if (character.Map.Id != packet.MapId)
            {
                Log.Warning("Incorrect map id when moving pets");
                return;
            }
            
            foreach (PtCtlSub sub in packet.Pets)
            {
                if (partner != null && partner.Entity.Id == sub.EntityId)
                {
                    partner.Entity.Position = sub.Position;
                    partner.Entity.Speed = packet.Speed;
                }
                
                if (pet != null && pet.Entity.Id == sub.EntityId)
                {
                    pet.Entity.Position = sub.Position;
                    pet.Entity.Speed = packet.Speed;
                }
            }
        }
    }
}