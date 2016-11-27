using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Abstractions.Services;
using Abstractions.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomegearUI.Controllers.Api
{

    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private IDevicesService devicesService;
        public DevicesController(IDevicesService devicesService)
        {
            this.devicesService = devicesService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<HomegearDevice> Get()
        {
            return devicesService.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
