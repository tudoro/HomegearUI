using System.Collections.Generic;

namespace Abstractions.Models
{
    public enum HomegearDeviceTypes
    {
        LightSwitch = 105
    }

    public class HomegearDevice
    {
        private readonly IDictionary<int, string> deviceTypeEntryPoints = new Dictionary<int, string>
        {
            { (int)HomegearDeviceTypes.LightSwitch, "lightswitches" }
        };

        public int Id { get; set; }
        public int TypeID { get; set; }
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
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public string TypeString { get; set; }
    }
}