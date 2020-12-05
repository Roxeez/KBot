using System;

namespace KBot.Data
{
    public sealed class DatabaseException : Exception
    {
        public DatabaseException(string message) : base(message)
        {
            
        }
    }
}