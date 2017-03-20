using System.Collections.Generic;
using Abstractions.Models;

namespace Abstractions.Services
{
    /// <summary>
    /// Manages the devices
    /// </summary>
    public interface IDevicesService<DeviceModel>
    {        
        /// <summary>
        /// Fetches all the lightswitches registered in the Homegear system
        /// </summary>
        /// <returns>All the lightswitches</returns>
        IEnumerable<DeviceModel> GetAll();

        /// <summary>
        /// Returns the light switch which matches the peerId
        /// </summary>
        /// <param name="id">The Id of the light switch</param>
        /// <returns>The light switch for the given peerId</returns>
        DeviceModel GetById(int id);

        /// <summary>
        /// Updates the parameters of a device
        /// </summary>
        /// <param name="deviceModel">The device model</param>
        void UpdateDevice(DeviceModel deviceModel);
    }
}
