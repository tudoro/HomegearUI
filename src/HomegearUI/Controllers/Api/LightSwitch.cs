using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Abstractions.Services;
using Abstractions.Models;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomegearUI.Controllers.Api
{
    [Route("api/[controller]")]
    public class LightSwitch : Controller
    {
        private readonly ILightSwitchService lightSwitchService;

        public LightSwitch(ILightSwitchService lightSwitchService)
        {
            this.lightSwitchService = lightSwitchService;
        }

        [HttpGet]
        public IEnumerable<LightSwitchModel> GetAll()
        {
            return lightSwitchService.GetAll();
        }

        [HttpGet("{id}", Name = "GetLightSwitch")]
        public IActionResult GetById(int id)
        {
            var lightSwitchItem = lightSwitchService.GetByPeerId(id);
            if (lightSwitchItem == null)
            {
                return NotFound();
            }
            return new ObjectResult(lightSwitchItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LightSwitchModel lightSwitch)
        {
            if (lightSwitch == null || lightSwitch.Id != id)
            {
                return BadRequest();
            }
            lightSwitchService.SetStateForPeerId(id, lightSwitch.State);
            return new NoContentResult();
        }


        //[HttpGet]
        //[Route("turnon")]
        //public void TurnOn()
        //{
        //    lightSwitchService.TurnOn();
        //}

        //[HttpGet]
        //[Route("turnoff")]
        //public void TurnOff(string id)
        //{
        //    lightSwitchService.TurnOff();
        //}

        //[HttpGet]
        //[Route("status")]
        //public bool Status(string id, string id2)
        //{
        //    return lightSwitchService.GetStatus();
        //}
    }
}
