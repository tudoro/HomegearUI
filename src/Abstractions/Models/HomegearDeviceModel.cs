﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Abstractions.Models
{
    /// <summary>
    /// All the supported device types. The value is the TypeID from the Device
    /// </summary>
    public enum HomegearDeviceTypes
    {
        LightSwitch = 105
    }

    /// <summary>
    /// The Homegear generic device, with common properties
    /// that are available for all Homematic devices
    /// </summary>
    public class HomegearDeviceModel
    {
        /// <summary>
        /// REST API entry points for different device types,
        /// used by the getter of the property <see cref="TypeID"/>
        /// </summary>
        private readonly IDictionary<HomegearDeviceTypes, string> deviceTypeEntryPoints = new Dictionary<HomegearDeviceTypes, string>
        {
            { HomegearDeviceTypes.LightSwitch, "lightswitches" }
        };

        /// <summary>
        /// Unique identifier in the Homegear
        /// </summary>

        public int Id { get; set; }
        /// <summary>
        /// The type of device
        /// </summary>

        [Editable(false)]
        public HomegearDeviceTypes TypeID { get; set; }
        /// <summary>
        /// The REST API endpoint to access more information about this device, taking into consideration its type
        /// <example>
        /// lightswitch/1
        /// </example>
        /// </summary>

        [Editable(false)]
        public string RestApiEntryPoint
        {
            get 
            {
                string entryPoint;
                if (deviceTypeEntryPoints.TryGetValue(this.TypeID, out entryPoint))
                {
                    return $"{entryPoint}/{Id}";
                }
                return "";
            }
        }
        /// <summary>
        /// Serial number of the device
        /// </summary>

        [Editable(false)]
        public string SerialNumber { get; set; }
        /// <summary>
        /// Name of the device as given in Homegear
        /// </summary>

        public string Name { get; set; }
        /// <summary>
        /// The type of device as a string
        /// </summary>

        [Editable(false)]
        public string TypeString { get; set; }
    }
}