using System;

namespace KBot.Game
{
    public readonly struct Position : IEquatable<Position>
    {
        private static readonly double Sqrt = Math.Sqrt(2);
        
        public short X { get; }
        public short Y { get; }

        public Position(short x, short y)
        {
            X = x;
            Y = y;
        }

        public double GetDistance(Position destination)
        {
            int x = Math.Abs(X - destination.X);
            int y = Math.Abs(Y - destination.Y);

            int min = Math.Min(x, y);
            int max = Math.Max(x, y);

            return (int)(min * Sqrt + max - min);
        }

        public bool IsBetween(Position firstPoint, Position secondPoint)
        {
            int minX = Math.Min(firstPoint.X, secondPoint.X);
            int maxX = Math.Min(firstPoint.X, secondPoint.X);
            
            int minY = Math.Min(firstPoint.Y, secondPoint.Y);
            int maxY = Math.Min(firstPoint.Y, secondPoint.Y);

            return X >= minX && X <= maxX && Y >= minY && Y <= maxY;
        }
        
        public bool Equals(Position other)
        {
            return other.X == X && other.Y == Y;
        }

        public override string ToString()
        {
            return $"{X}/{Y}";
        }
    }
}