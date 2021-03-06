﻿using System;
using Abstractions.Models;
using Abstractions.Services;
using DB.Contexts;
using DB.Models;
using System.Linq;
using Abstractions.Models.Options;
using Microsoft.Extensions.Options;

namespace DB.Services
{
    public class DBLightSwitchesPersistenceService : ILightSwitchesPersistenceService
    {
        private readonly HomegearDevicesContext _dbContext;
        private readonly LightSwitchOptions _lightSwitchOptions;

        public DBLightSwitchesPersistenceService(HomegearDevicesContext dbContext, IOptions<LightSwitchOptions> lightSwitchOptions)
        {
            _dbContext = dbContext;
            _lightSwitchOptions = lightSwitchOptions.Value;
        }

        public void LogUpdateOnLightswitch(LightSwitchModel lightSwitch)
        {
            bool addNewRecord = true;

            // Check if at least a predefined ammount of time passed since the last record with the same state was inserted.
            // There can be the case when the same state is being triggered for a light switch multiple times in a short time span. 
            // We don't want to log all of those events.
            var latestLightSwitchRecord = _dbContext.LightSwitches
                .OrderByDescending(l => l.ExecutionDateTime)
                .FirstOrDefault();

            if (latestLightSwitchRecord != null && latestLightSwitchRecord.State == lightSwitch.State)
            {
                TimeSpan timeElapsedSinceLastRecord = DateTime.Now.Subtract(latestLightSwitchRecord.ExecutionDateTime);
                TimeSpan buffer = new TimeSpan(0, 0, _lightSwitchOptions.NoLogUpdateInterval);

                if (timeElapsedSinceLastRecord < buffer)
                {
                    addNewRecord = false;
                }
            }

            if (addNewRecord)
            {
                _dbContext.LightSwitches.Add(new LightSwitch
                {
                    LightSwitchId = lightSwitch.Id,
                    ExecutionDateTime = DateTime.Now,
                    State = lightSwitch.State
                });
                _dbContext.SaveChanges();
            }
        }
    }
}
