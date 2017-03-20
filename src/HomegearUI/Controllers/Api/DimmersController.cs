using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Abstractions.Models;
using Abstractions.Services;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomegearUI.Controllers.Api
{
    [Route("api/[controller]")]
    public class DimmersController : Controller
    {
        private readonly IDevicesService<DimmerModel> _dimmerService;

        public DimmersController(IDevicesService<DimmerModel> dimmerService)
        {
            _dimmerService = dimmerService;
        }

        [HttpGet]
        public IEnumerable<DimmerModel> GetAll()
        {
            return _dimmerService.GetAll();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DimmerModel dimmer)
        {
            try
            {
                dimmer.Id = id;
                _dimmerService.UpdateDevice(dimmer);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest();
            }
        }
    }
}
