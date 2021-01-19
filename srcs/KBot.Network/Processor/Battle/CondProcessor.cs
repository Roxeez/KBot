using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Extension;
using KBot.Game.Maps;
using KBot.Network.Packet.Battle;

namespace KBot.Network.Processor.Battle
{
    public class CondProcessor : PacketProcessor<Cond>
    {
        protected override void Process(GameSession session, Cond packet)
        {
            Character character = session.Character;
            Map map = character.Map;

            LivingEntity entity = map.GetEntity<LivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                return;
            }
            
            entity.Speed = packet.Speed;
            entity.CantAttack = packet.CantAttack;
            entity.CantMove = packet.CantMove;
        }
    }
}