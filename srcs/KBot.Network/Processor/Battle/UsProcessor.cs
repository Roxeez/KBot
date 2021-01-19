using KBot.Common.Logging;
using KBot.Game;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class UsProcessor : PacketProcessor<Us>
    {
        protected override void Process(GameSession session, Us packet)
        {
            Log.Information($"Used skill {packet.CastId} on {packet.EntityType} with ID {packet.EntityId}");
        }
    }
}