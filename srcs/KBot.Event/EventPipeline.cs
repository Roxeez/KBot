using System;
using System.Collections.Generic;
using System.Linq;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Game;

namespace KBot.Event
{
    public sealed class EventPipeline
    {
        private readonly Dictionary<Type, IEventProcessor> processors;

        public EventPipeline()
        {
            processors = new Dictionary<Type, IEventProcessor>();
        }

        public void Process(GameSession session, IEvent e)
        {
            IEventProcessor processor = processors.GetValue(e.GetType());
            if (processor == null)
            {
                return;
            }

            processor.Process(session, e);
        }

        public void AddProcessor(IEventProcessor processor)
        {
            processors[processor.EventType] = processor;
        }
    }
}