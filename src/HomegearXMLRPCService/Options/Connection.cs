using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomegearXMLRPCService.Options
{
    public class Connection
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public string EventListenerHostname { get; set; }
        public string EventListenerIP { get; set; }
        public int EventListenerPort { get; set; }
    }
}
