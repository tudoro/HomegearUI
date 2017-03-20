namespace Abstractions.Models
{
    public static class ExternalWallSocketVariables
    {
        public static readonly string CURRENT = "CURRENT";
        public static readonly string VOLTAGE = "VOLTAGE";
        public static readonly string ENERGY_COUNTER = "ENERGY_COUNTER";
        public static readonly string FREQUENCY = "FREQUENCY";
        public static readonly string STATE = "STATE";
    }

    public class ExternalWallSocketModel
    {
        public int Id { get; set; }

        public bool? State { get; set; }

        public double? Voltage { get; set; }

        public double? Current { get; set; }

        public double? Frequency { get; set; }

        public double? EnergyCounter { get; set; }
    }
}
