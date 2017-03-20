using Abstractions.Models;

namespace Abstractions.Services
{
    public interface ILightSwitchesPersistenceService
    {
        void LogUpdateOnLightswitch(LightSwitchModel lightSwitch);
    }
}
