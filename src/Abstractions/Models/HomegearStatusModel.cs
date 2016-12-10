using System.ComponentModel.DataAnnotations;

namespace Abstractions.Models
{
    /// <summary>
    /// Holds information about the current status of the connection
    /// to Homegear Server
    /// </summary>
    public class HomegearStatusModel
    {
        /// <summary>
        /// True if connected, False otherwise
        /// </summary>
        [Editable(false)]
        public bool IsConnected { get; set; }
    }
}
