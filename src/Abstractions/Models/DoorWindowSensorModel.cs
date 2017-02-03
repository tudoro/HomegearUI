using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abstractions.Models
{
    public class DoorWindowSensorModel
    {
        /// <summary>
        /// Unique identifier of this device.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Current state of the device.
        /// True if door/window is open, False otherwise.
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// Current state of the battery.
        /// True if battery is low, False otherwise.
        /// </summary>
        public bool LowBattery { get; set; }

    }
}
