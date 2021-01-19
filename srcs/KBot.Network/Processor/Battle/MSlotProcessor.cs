using System.Linq;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class MSlotProcessor : PacketProcessor<MSlot>
    {
        protected override void Process(GameSession session, MSlot packet)
        {
            Skill skill = session.Character.Skills.FirstOrDefault(x => x.CastId == packet.CastId);
            if (skill == null)
            {
                Log.Warning($"Can't found combo skill {packet.CastId}");
                return;
            }

            session.Character.ComboSkill = skill;
        }
    }
}