using System.Linq;
using KBot.Common.Logging;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class SrProcessor : PacketProcessor<Sr>
    {
        protected override void Process(GameSession session, Sr packet)
        {
            Skill skill = session.Character.Skills.FirstOrDefault(x => x.CastId == packet.CastId);
            if (skill == null)
            {
                Log.Warning($"Can't found skill with cast id {packet.CastId} to reset cooldown");
                return;
            }

            skill.IsOnCooldown = false;
        }
    }
}