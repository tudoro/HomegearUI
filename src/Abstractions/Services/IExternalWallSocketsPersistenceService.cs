using Abstractions.Models;

namespace Abstractions.Services
{
    public interface IExternalWallSocketsPersistenceService
    {
        void LogValue(ExternalWallSocketModel wallSocket); 
    }
}
