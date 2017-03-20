using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Abstractions.Services;
using Abstractions.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomegearUI.Controllers.Api
{
    /// <summary>
    /// Manages the requests for the devices in the system
    /// </summary>
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        /// <summary>
        /// Reference to the service that communicates with Homegear
        /// </summary>
        private IDevicesService<HomegearDeviceModel> _devicesService;

        public DevicesController(IDevicesService<HomegearDeviceModel> devicesService)
        {
            this._devicesService = devicesService;
        }
        
        /// <summary>
        /// Gets all the devices
        /// </summary>
        /// <returns>The devices</returns>
        // GET: api/values
        [HttpGet]
        public IEnumerable<HomegearDeviceModel> Get()
        {
            return _devicesService.GetAll();
        }

        /// <summary>
        /// Updates a device
        /// </summary>
        /// <param name="id">The id of the device</param>
        /// <param name="homegearDevice">The device data to be updated</param>
        /// <returns>The updated device</returns>
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] HomegearDeviceModel homegearDevice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    homegearDevice.Id = id;
                    _devicesService.UpdateDevice(homegearDevice);
                    return NoContent();
                }
                catch (KeyNotFoundException e)
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
