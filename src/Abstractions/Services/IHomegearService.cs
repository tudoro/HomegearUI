using Abstractions.Models;

namespace Abstractions.Services
{
    public interface IHomegearService
    {
        HomegearStatusModel GetStatus();
    }
}
