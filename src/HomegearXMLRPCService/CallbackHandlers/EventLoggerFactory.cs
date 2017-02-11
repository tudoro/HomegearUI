using System.Collections.Generic;
using HomegearXMLRPCService.CallbackHandlers.Loggers;
using Abstractions.Models;
using Microsoft.Extensions.Logging;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class EventLoggerFactory
    {
        
        private struct EventType
        {
            public HomegearDeviceTypes DeviceType;
            public string VariableName;
        }

        private readonly Dictionary<EventType, IEventLogger> _eventLoggers = new Dictionary<EventType, IEventLogger>();

        public void RegisterEventLogger(HomegearDeviceTypes deviceType, string variableName, IEventLogger eventLogger)
        {
            EventType eventType;
            eventType.DeviceType = deviceType;
            eventType.VariableName = variableName;
            _eventLoggers.Add(eventType, eventLogger);
        }

        public IEventLogger GetEventLoggerFor(HomegearDeviceTypes deviceType, string variableName)
        {
            EventType eventType;
            eventType.DeviceType = deviceType;
            eventType.VariableName = variableName;
            IEventLogger eventLogger; 

            if (_eventLoggers.TryGetValue(eventType, out eventLogger))
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
