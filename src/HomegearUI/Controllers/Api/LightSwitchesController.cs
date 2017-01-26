using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Abstractions.Services;
using Abstractions.Models;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomegearUI.Controllers.Api
{
    [Route("api/[controller]")]
    public class LightSwitchesController : Controller
    {
        /// <summary>
        /// Reference to the service communicating to the Homegear server
        /// </summary>
        private readonly ILightSwitchesService _lightSwitchesService;

        public LightSwitchesController(ILightSwitchesService lightSwitchService)
        {
            this._lightSwitchesService = lightSwitchService;
        }
        
        /// <summary>
        /// Gets all the lightswitches
        /// </summary>
        /// <returns>The lightswitches</returns>
        [HttpGet]
        public IEnumerable<LightSwitchModel> GetAll()
        {
            return _lightSwitchesService.GetAll();
        }

        /// <summary>
        /// Gets a specific lightswitch
        /// </summary>
        /// <param name="id">The id of the lightswitch to get</param>
        /// <returns>The lightswitch</returns>
        [HttpGet("{id}", Name = "GetLightSwitch")]
        public IActionResult GetById(int id)
        {
            try
            {
                var lightSwitchItem = _lightSwitchesService.GetById(id);
                return new ObjectResult(lightSwitchItem);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }            
        }

        /// <summary>
        /// Updates the lightswich device
        /// </summary>
        /// <param name="id">The id of the lightswitch to update</param>
        /// <param name="lightSwitch">The update data of the lightswitch</param>
        /// <returns>Returns an empty result with HTTP 201 status in case of success</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LightSwitchModel lightSwitch)
        {
            try
            {
                _lightSwitchesService.SetStateForId(id, lightSwitch.State);
                return NoContent();
            }
            catch (KeyNotFoundException e)
            {
                return BadRequest();
            }
        }
    }
}
