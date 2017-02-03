using Abstractions.Services;
using Abstractions.Models;
using HomegearLib;
using System;

namespace HomegearXMLRPCService.CallbackHandlers.Loggers
{
    public class DoorWindowSensorLowBatteryEventLogger: IEventLogger
    {
        private readonly IDoorWindowSensorActivityService _doorWindowSensorActivityService;
        public DoorWindowSensorLowBatteryEventLogger(IDoorWindowSensorActivityService doorWindowSensorActivityService)
        {
            _doorWindowSensorActivityService = doorWindowSensorActivityService;
        }

        public void LogEvent(Device device, Variable variable)
        {
            _doorWindowSensorActivityService.LogLowBatteryStateOnSensor(new DoorWindowSensorModel
            {
                Id = device.ID,
                State = variable.Name == "STATE" ? variable.BooleanValue : false,
                LowBattery = variable.Name == "LOWBAT" ? variable.BooleanValue : false
            });
        }
    }
}
