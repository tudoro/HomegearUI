using Abstractions.Models;
using Abstractions.Services;
using DB.Contexts;
using DB.Models;
using System;

namespace DB.Services
{
    public class DBDoorWindowSensorActivityService : IDoorWindowSensorPersistenceService
    {
        private readonly HomegearDevicesContext _dbContext;

        public DBDoorWindowSensorActivityService(HomegearDevicesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogLowBatteryStateOnSensor(DoorWindowSensorModel doorWindowSensor)
        {
            _dbContext.DoorWindowSensorActivity.Add(new DoorWindowSensorActivity
            {
                SensorId = doorWindowSensor.Id,
                ExecutionDateTime = DateTime.Now,
                State = doorWindowSensor.State,
                LowBattery = null
            });
            _dbContext.SaveChanges();
        }

        public void LogOpenCloseStateOnSenor(DoorWindowSensorModel doorWindowSensor)
        {
            _dbContext.DoorWindowSensorActivity.Add(new DoorWindowSensorActivity
            {
                SensorId = doorWindowSensor.Id,
                ExecutionDateTime = DateTime.Now,
                State = null,
                LowBattery = doorWindowSensor.LowBattery
            });
            _dbContext.SaveChanges();
        }
    }
}
