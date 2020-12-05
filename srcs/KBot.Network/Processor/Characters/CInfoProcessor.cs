using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Networking.Packet.Player;

namespace KBot.Networking.Processor.Characters
{
    public class CInfoProcessor : PacketProcessor<CInfo>
    {
        protected override void Process(GameSession session, CInfo packet)
        {
            Character character = session.Character;

            character.Id = packet.Id;
            character.Name = packet.Name;
            character.Gender = packet.Gender;
            character.Job = packet.Job;
            
            Log.Debug("Character basic information successfully set");
        }
    }
}