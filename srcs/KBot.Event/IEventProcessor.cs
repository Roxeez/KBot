using System;
using KBot.Game;

namespace KBot.Event
{
    public interface IEventProcessor
    {
        Type EventType { get; }
        
        void Process(GameSession session, IEvent e);
    }

    public abstract class EventProcessor<T> : IEventProcessor where T : IEvent
    {
        public Type EventType { get; } = typeof(T);
        
        public void Process(GameSession session, IEvent e)
        {
            Process(session, (T)e);
        }

        protected abstract void Process(GameSession session, T e);
    }
}