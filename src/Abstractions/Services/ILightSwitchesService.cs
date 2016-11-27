using System.Collections.Generic;
using Abstractions.Models;

namespace Abstractions.Services
{
    public interface ILightSwitchesService
    {        
        IEnumerable<LightSwitchModel> GetAll();
        /// <summary>
        /// Returns the light switch which matches the peerId
        /// </summary>
        /// <param name="id">The Id of the light switch</param>
        /// <returns>The light switch for the given peerId</returns>
        LightSwitchModel GetById(int id);
        void SetStateForId(int id, bool state);
    }
}
