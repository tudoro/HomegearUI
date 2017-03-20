using Abstractions.Services;
using Abstractions.Models;
using HomegearLib;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class DoorWindowSensorStateEventHandler : IEventHandler
    {
        private readonly IDoorWindowSensorPersistenceService _doorWindowSensorActivityService;
        private readonly IDevicesService<LightSwitchModel> _lightSwitchesService;

        public DoorWindowSensorStateEventHandler(IDoorWindowSensorPersistenceService doorWindowSensorActivityService, IDevicesService<LightSwitchModel> lightSwitchesService)
        {
            _doorWindowSensorActivityService = doorWindowSensorActivityService;
            _lightSwitchesService = lightSwitchesService;
        }

        public void LogEvent(int deviceId, string variableName, dynamic variableValue)
        {
            _doorWindowSensorActivityService.LogOpenCloseStateOnSenor(new DoorWindowSensorModel
            {
                Id = deviceId,
                State = variableName == DoorWindowSensorVariables.STATE ? variableValue : false,
                LowBattery = variableName == DoorWindowSensorVariables.LOWBAT ? variableValue : false,
            });
            //_lightSwitchesService.UpdateDevice(new LightSwitchModel {
            //    Id = 1,
            //    State = true
            //});
        }
    }
}
