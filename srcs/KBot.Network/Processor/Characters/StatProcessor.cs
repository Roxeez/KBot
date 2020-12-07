using KBot.Common.Extension;
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

            character.HpPercentage = character.Hp.GetPercentage(character.HpMaximum);
            character.MpPercentage = character.Mp.GetPercentage(character.MpMaximum);
            
            Log.Trace("Character hp/mp updated completed");
        }
    }
}