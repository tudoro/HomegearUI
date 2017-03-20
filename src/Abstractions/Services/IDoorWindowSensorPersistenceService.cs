using Abstractions.Models;

namespace Abstractions.Services
{
    public interface IDoorWindowSensorPersistenceService
    {
        void LogOpenCloseStateOnSenor(DoorWindowSensorModel doorWindowSensor);
        void LogLowBatteryStateOnSensor(DoorWindowSensorModel doorWindowSensor);
    }
}
