using Abstractions.Models;

namespace Abstractions.Services
{
    public interface ILightSwitchesPersistenceService
    {
        void LogActionOnLightswitch(LightSwitchModel lightSwitch);
    }
}
