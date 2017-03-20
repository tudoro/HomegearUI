using System;
using System.Collections.Generic;
using Abstractions.Services;
using Abstractions.Models;
using HomegearLib;

namespace HomegearXMLRPCService.Services
{
    public class XMLRPCDimmersService : IDevicesService<DimmerModel>
    {
        private readonly Homegear _homegear;

        public XMLRPCDimmersService(Homegear homegear)
        {
            _homegear = homegear;
        }

        public IEnumerable<DimmerModel> GetAll()
        {
            List<DimmerModel> dimmers = new List<DimmerModel>();
            foreach (KeyValuePair<int, Device> device in _homegear.Devices)
            {
                if (device.Value.TypeID == (int)HomegearDeviceTypes.Dimmer)
                {
                    dimmers.Add(new DimmerModel
                    {
                        Id = device.Key,
                        Level = device.Value.Channels[1].Variables["LEVEL"].DoubleValue
                    });
                }
            }
            return dimmers;
        }

        public DimmerModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDevice(DimmerModel deviceModel)
        {
            try
            {
                Device device = GetDeviceForId(deviceModel.Id);
                if (device.TypeID != (int)HomegearDeviceTypes.Dimmer)
                {
                    throw new KeyNotFoundException();
                }
                device.Channels[1].Variables["LEVEL"].DoubleValue = deviceModel.Level;
                //_lightSiwtchPersistence.LogUpdateOnLightswitch(lightSwitch);
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
            if (_homegear.Devices.TryGetValue(id, out device))
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
