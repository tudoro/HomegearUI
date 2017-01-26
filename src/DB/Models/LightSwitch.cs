using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Models
{
    public class LightSwitch
    {
        public int Id { get; set; }
        public int LightSwitchId { get; set; }
        public DateTime ExecutionDateTime { get; set; }
        public bool State { get; set; }
    }
}
