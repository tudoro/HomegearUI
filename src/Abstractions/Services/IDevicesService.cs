using Abstractions.Models;
using System.Collections.Generic;

namespace Abstractions.Services
{
    /// <summary>
    /// Manages all <see cref="HomegearDeviceModel"/> in the system
    /// </summary>
    public interface IDevicesService
    {
        /// <summary>
        /// Fetches all the devices registered with the Homegear system
        /// </summary>
        /// <returns>The devices in the system</returns>
        IEnumerable<HomegearDeviceModel> GetAll();

        /// <summary>
        /// Updates a device
        /// </summary>
        /// <param name="device">The device to update</param>
        /// <returns>The updated device</returns>
        HomegearDeviceModel UpdateDevice(HomegearDeviceModel device);
    }
}
