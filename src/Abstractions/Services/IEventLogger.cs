namespace Abstractions.Services
{
    public interface IEventHandler
    {
        void LogEvent(int deviceId, string variableName, dynamic variableValue);
    }
}
