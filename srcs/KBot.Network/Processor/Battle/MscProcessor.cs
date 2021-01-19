using KBot.Common.Logging;
using KBot.Game;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class MscProcessor : PacketProcessor<Msc>
    {
        protected override void Process(GameSession session, Msc packet)
        {
            if (packet.Undefined == 0)
            {
                session.Character.ComboSkill = null;
                Log.Debug("Combo skill removed");
            }
        }
    }
}