using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Abstractions.Models.Options
{
    /// <summary>
    /// Light Switch configuration options. 
    /// </summary>
    public class LightSwitchOptions
    {
        /// <summary>
        /// The number of seconds between log entries for a lightwitch state change.
        /// This prevents too many entries to be created when the lightswitch is changing the state very fast.
        /// As a rule of thumb, do not put this value too low.
        /// </summary>
        public int NoLogUpdateInterval { get; set; }
    }
}
