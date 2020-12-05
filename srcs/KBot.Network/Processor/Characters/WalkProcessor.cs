using KBot.Game;
using KBot.Game.Entities;
using KBot.Network.Packet.Characters;

namespace KBot.Network.Processor.Characters
{
    public class WalkProcessor : PacketProcessor<Walk>
    {
        protected override void Process(GameSession session, Walk packet)
        {
            Character character = session.Character;

            character.Position = packet.Position;
            character.Speed = packet.Speed;
        }
    }
}