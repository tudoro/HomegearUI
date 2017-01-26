using System.Collections.Generic;
using HomegearXMLRPCService.CallbackHandlers.Loggers;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class EventLoggerFactory
    {
        private readonly Dictionary<string, IEventLogger> _eventLoggers = new Dictionary<string, IEventLogger>();

        public void RegisterEventLogger(string eventName, IEventLogger eventLogger)
        {
            _eventLoggers.Add(eventName, eventLogger);
        }

        public IEventLogger GetEventLoggerFor(string eventName)
        {
            IEventLogger eventLogger; 
            if (_eventLoggers.TryGetValue(eventName, out eventLogger))
            {
                return eventLogger;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
