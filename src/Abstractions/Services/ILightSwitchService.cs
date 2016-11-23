using System;
using System.Collections.Generic;
using Abstractions.Models;

namespace Abstractions.Services
{
    public interface ILightSwitchService
    {
        
        IEnumerable<LightSwitchModel> GetAll();

        /// <summary>
        /// Returns the light switch which matches the peerId
        /// </summary>
        /// <param name="peerId">The Id of the light switch</param>
        /// <returns>The light switch for the given peerId</returns>
        LightSwitchModel GetByPeerId(int peerId);
        void SetStateForPeerId(int id, bool state);
    }
}
