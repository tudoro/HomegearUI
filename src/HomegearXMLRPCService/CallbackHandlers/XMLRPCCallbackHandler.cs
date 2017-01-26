using HomegearXMLRPCService.CallbackHandlers.Loggers;
using HomegearLib;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class XMLRPCCallbackHandler
    {
        public IEventLogger EventLogger { get; set; }

        public void LogEvent(Device device, Variable variable)
        {
            EventLogger.LogEvent(device, variable);
        }
    }
}
