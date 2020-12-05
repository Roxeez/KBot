using System;

namespace KBot.Game.Entities
{
    public sealed class Character : Player
    {
        public int Hp { get; set; }
        public int HpMaximum { get; set; }
        public int Mp { get; set; }
        public int MpMaximum { get; set; }

        private readonly GameSession session;
        
        public Character(GameSession session)
        {
            this.session = session;
        }
    }
}