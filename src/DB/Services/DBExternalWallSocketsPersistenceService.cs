using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions.Models;
using Abstractions.Services;
using DB.Contexts;
using DB.Models;

namespace DB.Services
{
    public class DBExternalWallSocketsPersistenceService : IExternalWallSocketsPersistenceService
    {
        private readonly HomegearDevicesContext _dbContext;

        public DBExternalWallSocketsPersistenceService(HomegearDevicesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogValue(ExternalWallSocketModel wallSocket)
        {
            _dbContext.ExternalWallSockets.Add(new ExternalWallSocket {
                ExternalWallSocketId = wallSocket.Id,
                ExecutionDateTime = DateTime.Now,
                Voltage = wallSocket.Voltage,
                Current  = wallSocket.Current,
                Frequency = wallSocket.Frequency,
                EnergyCounter = wallSocket.EnergyCounter
            });
            _dbContext.SaveChanges();
        }
    }
}
