using HomegearXMLRPCService.CallbackHandlers.Loggers;
using HomegearLib;
using Microsoft.Extensions.Logging;

namespace HomegearXMLRPCService.CallbackHandlers
{
    public class XMLRPCCallbackHandler
    {
        private readonly ILogger _logger;
        public IEventLogger EventLogger { get; set; }

        public XMLRPCCallbackHandler(ILogger logger)
        {
            _logger = logger;
        }

        public void LogEvent(Device device, Variable variable)
        {
            if (variable.StringValue != "")
            {
                _logger.LogDebug("Logging event for device id {0}, variable name {1}, variable value {2}.", device.TypeID, variable.Name, variable.StringValue);
            }
            else
            {
                _logger.LogDebug("Logging event for device id {0}, variable name {1}, variable value {2}.", device.TypeID, variable.Name, variable.BooleanValue);
            }
            EventLogger.LogEvent(device, variable);
        }
    }
}
