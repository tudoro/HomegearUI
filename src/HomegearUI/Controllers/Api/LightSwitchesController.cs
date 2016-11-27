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
        private readonly ILightSwitchesService lightSwitchesService;

        public LightSwitchesController(ILightSwitchesService lightSwitchService)
        {
            this.lightSwitchesService = lightSwitchService;
        }

        [HttpGet]
        public IEnumerable<LightSwitchModel> GetAll()
        {
            return lightSwitchesService.GetAll();
        }

        [HttpGet("{id}", Name = "GetLightSwitch")]
        public IActionResult GetById(int id)
        {
            try
            {
                var lightSwitchItem = lightSwitchesService.GetById(id);
                return new ObjectResult(lightSwitchItem);
            }
            catch (KeyNotFoundException e)
            {
                return new NotFoundResult();
            }            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LightSwitchModel lightSwitch)
        {
            try
            {
                lightSwitchesService.SetStateForId(id, lightSwitch.State);
                return new NoContentResult();
            }
            catch (KeyNotFoundException e)
            {
                return new BadRequestResult();
            }
        }
    }
}
