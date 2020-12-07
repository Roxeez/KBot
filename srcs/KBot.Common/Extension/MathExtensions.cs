namespace KBot.Common.Extension
{
    public static class MathExtensions
    {        
        public static int GetPercentage(this int value, int max)
        {
            return value == 0 || max == 0 ? 0 : (value * 100) / max;
        }
    }
}