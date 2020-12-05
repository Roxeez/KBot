using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Network.Packet.Characters;

namespace KBot.Network.Processor.Characters
{
    public class StatProcessor : PacketProcessor<Stat>
    {
        protected override void Process(GameSession session, Stat packet)
        {
            Character character = session.Character;

            character.Hp = packet.Hp;
            character.Mp = packet.Mp;
            character.HpMaximum = packet.HpMaximum;
            character.MpMaximum = packet.MpMaximum;

            character.HpPercentage = character.Hp == 0 || character.HpMaximum == 0 ? 0 : (character.Hp / character.HpMaximum) * 100;
            character.MpPercentage = character.Mp == 0 || character.MpMaximum == 0 ? 0 : (character.Mp / character.MpMaximum) * 100;
            
            Log.Trace("Character hp/mp updated completed");
        }
    }
}