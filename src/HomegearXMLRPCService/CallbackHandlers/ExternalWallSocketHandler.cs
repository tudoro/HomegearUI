using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomegearLib;
using Abstractions.Services;
using Abstractions.Models;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class ExternalWallSocketHandler : IEventHandler
    {
        private readonly IExternalWallSocketsPersistenceService _wallSocketsDBService;

        public ExternalWallSocketHandler(IExternalWallSocketsPersistenceService wallSocketsDBService)
        {
            _wallSocketsDBService = wallSocketsDBService;
        }

        public void LogEvent(int deviceId, string variableName, dynamic variableValue)
        {
            _wallSocketsDBService.LogValue(new ExternalWallSocketModel
            {
                Id = deviceId,
                Current = variableName == ExternalWallSocketVariables.CURRENT ? variableValue : (double?)null,
                Voltage = variableName == ExternalWallSocketVariables.VOLTAGE ? variableValue : (double?)null,
                Frequency = variableName == ExternalWallSocketVariables.FREQUENCY ? variableValue : (double?)null,
                EnergyCounter = variableName == ExternalWallSocketVariables.ENERGY_COUNTER ? variableValue : (double?)null
            });
        }
    }
}
