using System.Collections.Generic;
using Abstractions.Models;

namespace Abstractions.Services
{
    /// <summary>
    /// Manages the light switches of type Lightswitch <see cref="HomegearDeviceTypes.LightSwitch"/>
    /// </summary>
    public interface ILightSwitchesService
    {        
        /// <summary>
        /// Fetches all the lightswitches registered in the Homegear system
        /// </summary>
        /// <returns>All the lightswitches</returns>
        IEnumerable<LightSwitchModel> GetAll();

        /// <summary>
        /// Returns the light switch which matches the peerId
        /// </summary>
        /// <param name="id">The Id of the light switch</param>
        /// <returns>The light switch for the given peerId</returns>
        LightSwitchModel GetById(int id);

        /// <summary>
        /// Switches the light on or off
        /// </summary>
        /// <param name="id">The id of the lightswitch</param>
        /// <param name="state">The state to set. True for turning it on, False for off</param>
        void SetStateForId(int id, bool state);
    }
}
