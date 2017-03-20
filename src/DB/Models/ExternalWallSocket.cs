using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Models
{
    public class ExternalWallSocket
    {
        public int Id { get; set; }
        public int ExternalWallSocketId { get; set; }
        public double? Voltage { get; set; }
        public double? Current { get; set; }
        public double? Frequency { get; set; }
        public double? EnergyCounter { get; set; }
        public DateTime ExecutionDateTime { get; set; }
    }
}
