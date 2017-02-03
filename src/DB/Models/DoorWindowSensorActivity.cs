using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Models
{
    public class DoorWindowSensorActivity
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public DateTime ExecutionDateTime { get; set; }
        public bool? State { get; set; }
        public bool? LowBattery { get; set; }
    }
}
