using System.Collections.Generic;
using Abstractions.Models;
using Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class EventHandlerFactory
    {
        
        private struct EventType
        {
            public HomegearDeviceTypes DeviceType;
            public string VariableName;
        }

        private readonly Dictionary<EventType, IEventHandler> _eventLoggers = new Dictionary<EventType, IEventHandler>();

        public void RegisterEventLogger(HomegearDeviceTypes deviceType, string variableName, IEventHandler eventLogger)
        {
            EventType eventType;
            eventType.DeviceType = deviceType;
            eventType.VariableName = variableName;
            _eventLoggers.Add(eventType, eventLogger);
        }

        public IEventHandler GetEventLoggerFor(HomegearDeviceTypes deviceType, string variableName)
        {
            EventType eventType;
            eventType.DeviceType = deviceType;
            eventType.VariableName = variableName;
            IEventHandler eventLogger; 

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
