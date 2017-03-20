using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Abstractions.Models
{
    /// <summary>
    /// All the supported device types. The value is the TypeID from the Device.
    /// </summary>
    public enum HomegearDeviceTypes
    {
        Dimmer = 104,
        LightSwitch = 105,
        DoorWindowMagneticSensor = 177,
        ExternalWallSocket = 215
    }

    /// <summary>
    /// The Homegear generic device, with common properties
    /// that are available for all Homematic devices.
    /// </summary>
    public class HomegearDeviceModel
    {
        /// <summary>
        /// REST API entry points for different device types,
        /// used by the getter of the property <see cref="TypeID"/>.
        /// </summary>
        private readonly IDictionary<HomegearDeviceTypes, string> deviceTypeEntryPoints = new Dictionary<HomegearDeviceTypes, string>
        {
            { HomegearDeviceTypes.LightSwitch, "lightswitches" },
            { HomegearDeviceTypes.Dimmer, "dimmers" }
        };

        /// <summary>
        /// Unique identifier in the Homegear.
        /// </summary>

        public int Id { get; set; }
        /// <summary>
        /// The type of device
        /// </summary>

        [Editable(false)]
        public HomegearDeviceTypes TypeID { get; set; }

        /// <summary>
        /// The REST API endpoint to access more information about this device, taking into consideration its type.
        /// <example>
        /// The property can contain a URI like this: lightswitch/1.
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
        /// Serial number of the device.
        /// </summary>
        [Editable(false)]
        public string SerialNumber { get; set; }

        /// <summary>
        /// User defined name of the device as given in Homegear.
        /// <example>
        /// Can be something like "Light siwitch living room 1"
        /// </example>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of device as a string.
        /// </summary>
        [Editable(false)]
        public string TypeString { get; set; }
    }
}