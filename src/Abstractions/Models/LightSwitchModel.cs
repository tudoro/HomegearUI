namespace Abstractions.Models
{
    /// <summary>
    /// Represents the Light Switch device with type <see cref="HomegearDeviceTypes.LightSwitch"/>
    /// </summary>
    public class LightSwitchModel
    {
        /// <summary>
        /// Unique identifier of this device
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Current state of the device
        /// True if the light switch is ON, False otherwise
        /// </summary>
        public bool State { get; set; }
    }
}
