﻿using System.Collections.Generic;
using Abstractions.Services;
using Abstractions.Models;
using HomegearLib;

namespace HomegearXMLRPCService.Services
{
    /// <summary>
    /// Manages the lightswitches
    /// </summary>
    public class XMLRPCLightSwitchesService : ILightSwitchesService
    {
        /// <summary>
        /// Reference to the <see cref="Homegear"/> service
        /// </summary>
        private readonly Homegear homegear;

        public XMLRPCLightSwitchesService(Homegear homegear)
        {
            this.homegear = homegear;
        }

        /// <summary>
        /// Gets all the lightswitch devices of type <see cref="HomegearDeviceTypes.LightSwitch"/> 
        /// </summary>
        /// <returns>All the lightswitches</returns>
        public IEnumerable<LightSwitchModel> GetAll()
        {
            List<LightSwitchModel> lightSwitches = new List<LightSwitchModel>();
            foreach (KeyValuePair<int, Device> device in homegear.Devices)
            {
                if (device.Value.TypeID == (int)HomegearDeviceTypes.LightSwitch)
                {
                    lightSwitches.Add(new LightSwitchModel
                    {
                        Id = device.Key,
                        State = device.Value.Channels[1].Variables["STATE"].BooleanValue
                    });
                }
            }
            return lightSwitches;
        }

        /// <summary>
        /// Returns the light switch which matches the id
        /// </summary>
        /// <param name="id">The Id of the light switch</param>
        /// <returns>The light switch for the given peerId</returns>
        public LightSwitchModel GetById(int id)
        {
            try
            {
                Device device = GetDeviceForId(id);
                if (device.TypeID != (int)HomegearDeviceTypes.LightSwitch)
                {
                    throw new KeyNotFoundException();
                }
                return new LightSwitchModel
                {
                    Id = id,
                    State = device.Channels[1].Variables["STATE"].BooleanValue
                };
            }
            catch(KeyNotFoundException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Turns a lightswitch on or off
        /// </summary>
        /// <param name="id">The id of the device</param>
        /// <param name="state">True for turning on, False otherwise</param>
        public void SetStateForId(int id, bool state)
        {
            try
            {
                Device device = GetDeviceForId(id);
                if (device.TypeID == (int)HomegearDeviceTypes.LightSwitch)
                {
                    throw new KeyNotFoundException();
                }
                device.Channels[1].Variables["STATE"].BooleanValue = state;
            }
            catch (KeyNotFoundException e)
            {
                throw e;
            }
            
        }

        /// <summary>
        /// Helper to get a certain device from the collection of devices returned by the <see cref="Homegear"/> service 
        /// </summary>
        /// <param name="id">The id of the lightswitch to search in the collection</param>
        /// <returns>The device</returns>
        private Device GetDeviceForId(int id)
        {
            Device device;
            if (homegear.Devices.TryGetValue(id, out device))
            {
                return device;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

    }
}
