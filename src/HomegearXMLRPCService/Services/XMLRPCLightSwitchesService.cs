using System.Collections.Generic;
using Abstractions.Services;
using Abstractions.Models;
using HomegearLib;

namespace HomegearXMLRPCService.Services
{
    public class XMLRPCLightSwitchesService : ILightSwitchesService
    {
        private readonly Homegear homegear;

        public XMLRPCLightSwitchesService(Homegear homegear)
        {
            this.homegear = homegear;
        }

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
        /// Returns the light switch which matches the peerId
        /// </summary>
        /// <param name="id">The Id of the light switch</param>
        /// <returns>The light switch for the given peerId</returns>
        public LightSwitchModel GetById(int id)
        {
            try
            {
                Device device = GetDeviceForId(id);
                return new LightSwitchModel
                {
                    Id = id,
                    State = device.Channels[1].Variables["STATE"].BooleanValue
                };
            }
            catch(KeyNotFoundException e)
            {
                throw;
            }
        }

        public void SetStateForId(int id, bool state)
        {
            try
            {
                Device device = GetDeviceForId(id);
                device.Channels[1].Variables["STATE"].BooleanValue = state;
            }
            catch (KeyNotFoundException e)
            {
                throw;
            }
            
        }

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
