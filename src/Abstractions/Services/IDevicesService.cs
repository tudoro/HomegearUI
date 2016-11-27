using Abstractions.Models;
using System.Collections.Generic;

namespace Abstractions.Services
{
    public interface IDevicesService
    {
        IEnumerable<HomegearDevice> GetAll();
    }
}
