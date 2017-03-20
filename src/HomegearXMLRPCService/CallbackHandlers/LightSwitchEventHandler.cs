using HomegearLib;
using Abstractions.Services;
using Abstractions.Models;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class LightSwitchEventHandler : IEventHandler
    {
        private readonly ILightSwitchesPersistenceService _lightSwitchesPersistenceService;

        public LightSwitchEventHandler(ILightSwitchesPersistenceService lightSwitchesPersistenceService)
        {
            _lightSwitchesPersistenceService = lightSwitchesPersistenceService;
        }

        public void LogEvent(int deviceId, string variableName, dynamic variableValue)
        {
            _lightSwitchesPersistenceService.LogUpdateOnLightswitch(new LightSwitchModel
            {
                Id = deviceId,
                State = variableValue
            });
        }
    }
}
