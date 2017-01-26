using HomegearLib;
using Abstractions.Services;
using Abstractions.Models;

namespace HomegearXMLRPCService.CallbackHandlers.Loggers
{
    public class LightSwitchEventLogger : IEventLogger
    {
        private readonly ILightSwitchesPersistenceService _lightSwitchesPersistenceService;

        public LightSwitchEventLogger(ILightSwitchesPersistenceService lightSwitchesPersistenceService)
        {
            _lightSwitchesPersistenceService = lightSwitchesPersistenceService;
        }

        public void LogEvent(Device device, Variable variable)
        {
            _lightSwitchesPersistenceService.LogActionOnLightswitch(new LightSwitchModel
            {
                Id = device.ID,
                State = variable.BooleanValue
            });
        }
    }
}
