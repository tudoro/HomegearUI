using HomegearLib;

namespace HomegearXMLRPCService.CallbackHandlers.Loggers
{
    public interface IEventLogger
    {
        void LogEvent(Device device, Variable variable);
    }
}
