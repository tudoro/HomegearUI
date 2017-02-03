using Abstractions.Models;

namespace Abstractions.Services
{
    public interface IDoorWindowSensorActivityService
    {
        void LogOpenCloseStateOnSenor(DoorWindowSensorModel doorWindowSensor);
        void LogLowBatteryStateOnSensor(DoorWindowSensorModel doorWindowSensor);
    }
}
