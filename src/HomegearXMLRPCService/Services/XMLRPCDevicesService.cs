using System.Collections.Generic;
using Abstractions.Models;
using Abstractions.Services;
using HomegearLib;
using System;

namespace HomegearXMLRPCService.Services
{
    /// <summary>
    /// Used to manage the general information about the devices registered in the Homegear system.
    /// </summary>
    public class XMLRPCDevicesService : IDevicesService
    {
        /// <summary>
        /// Reference to the <see cref="Homegear"/> service that is used to provide
        /// data about each device in the system and manipulate it.
        /// </summary>
        private Homegear homegear;

        public XMLRPCDevicesService(Homegear homegear)
        {
            this.homegear = homegear;
        }

        /// <summary>
        /// Fetches all the devices registered in the system
        /// </summary>
        /// <returns>All the devices</returns>
        public IEnumerable<HomegearDeviceModel> GetAll()
        {
            List<HomegearDeviceModel> homegearDevices = new List<HomegearDeviceModel>();
            foreach (KeyValuePair<int, Device> device in homegear.Devices)
            {
                homegearDevices.Add(new HomegearDeviceModel
                {
                    Id = device.Key,
                    TypeID = (HomegearDeviceTypes)device.Value.TypeID,
                    TypeString = device.Value.TypeString,
                    Name = device.Value.Name,
                    SerialNumber = device.Value.SerialNumber
                });
            }
            return homegearDevices;
        }

        /// <summary>
        /// Updates information about a device
        /// </summary>
        /// <param name="deviceModel">The device information to update</param>
        /// <returns>The updated device</returns>
        public HomegearDeviceModel UpdateDevice(HomegearDeviceModel deviceModel)
        {
            Device foundDevice;
            if (homegear.Devices.TryGetValue(deviceModel.Id, out foundDevice))
            {
                foundDevice.Name = deviceModel.Name;
                return deviceModel;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
