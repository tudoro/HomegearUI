using Abstractions.Services;
using Abstractions.Models;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class DoorWindowSensorLowBatteryEventHandler: IEventHandler
    {
        private readonly IDoorWindowSensorPersistenceService _doorWindowSensorActivityService;
        public DoorWindowSensorLowBatteryEventHandler(IDoorWindowSensorPersistenceService doorWindowSensorActivityService)
        {
            _doorWindowSensorActivityService = doorWindowSensorActivityService;
        }

        public void LogEvent(int deviceId, string variableName, dynamic variableValue)
        {
            _doorWindowSensorActivityService.LogLowBatteryStateOnSensor(new DoorWindowSensorModel
            {
                Id = deviceId,
                State = variableName == DoorWindowSensorVariables.STATE ? variableValue : false,
                LowBattery = variableName == DoorWindowSensorVariables.LOWBAT ? variableValue : false
            });
        }
    }
}
