using System.Collections.Generic;
using Abstractions.Models;
using Abstractions.Services;
using HomegearLib;
using System;

namespace HomegearXMLRPCService.Services
{
    public class XMLRPCDevicesService : IDevicesService
    {
        private Homegear homegear;

        public XMLRPCDevicesService(Homegear homegear)
        {
            this.homegear = homegear;
        }

        public IEnumerable<HomegearDevice> GetAll()
        {
            List<HomegearDevice> homegearDevices = new List<HomegearDevice>();
            foreach (KeyValuePair<int, Device> device in homegear.Devices)
            {
                homegearDevices.Add(new HomegearDevice
                {
                    Id = device.Key,
                    TypeID = device.Value.TypeID,
                    TypeString = device.Value.TypeString,
                    Name = device.Value.Name,
                    SerialNumber = device.Value.SerialNumber
                });
            }
            return homegearDevices;
        }

    }
}
