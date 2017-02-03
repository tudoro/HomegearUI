using Abstractions.Services;
using Abstractions.Models;
using HomegearLib;

namespace HomegearXMLRPCService.CallbackHandlers.Loggers
{
    public class DoorWindowSensorStateEventLogger : IEventLogger
    {
        private readonly IDoorWindowSensorActivityService _doorWindowSensorActivityService;
        private readonly ILightSwitchesService _lightSwitchesService;

        public DoorWindowSensorStateEventLogger(IDoorWindowSensorActivityService doorWindowSensorActivityService, ILightSwitchesService lightSwitchesService)
        {
            _doorWindowSensorActivityService = doorWindowSensorActivityService;
            _lightSwitchesService = lightSwitchesService;
        }

        public void LogEvent(Device device, Variable variable)
        {
            _doorWindowSensorActivityService.LogOpenCloseStateOnSenor(new DoorWindowSensorModel
            {
                Id = device.ID,
                State = variable.Name == "STATE" ? variable.BooleanValue : false,
                LowBattery = variable.Name == "LOWBAT" ? variable.BooleanValue : false,
            });
            _lightSwitchesService.SetStateForId(1, true);
        }
    }
}
