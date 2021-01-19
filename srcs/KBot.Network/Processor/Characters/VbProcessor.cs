using System.Linq;
using KBot.Game;
using KBot.Game.Battle;
using KBot.Network.Packet.Characters;

namespace KBot.Network.Processor.Characters
{
    public class VbProcessor : PacketProcessor<Vb>
    {
        private readonly BuffFactory buffFactory;

        public VbProcessor(BuffFactory buffFactory)
        {
            this.buffFactory = buffFactory;
        }
        
        protected override void Process(GameSession session, Vb packet)
        {
            
            if (!packet.IsEnabled)
            {
                Buff buff = session.Character.Buffs.FirstOrDefault(x => x.Id == packet.BuffId);
                if (buff == null)
                {
                    return;
                }
                session.Character.Buffs.Remove(buff);
            }

            if (packet.IsEnabled)
            {
                Buff buff = buffFactory.CreateBuff(packet.BuffId, packet.Duration);
                if (buff == null)
                {
                    return;
                }
                
                session.Character.Buffs.Add(buff);
            }
        }
    }
}