using Abstractions.Models;

namespace Abstractions.Services
{
    /// <summary>
    /// Manages the connection to the Homegear Server
    /// </summary>
    public interface IHomegearConnectionService
    {
        /// <summary>
        /// Returns the current connection status
        /// </summary>
        /// <returns>The status of the connection to Homegear Server</returns>
        HomegearStatusModel GetStatus();
    }
}
