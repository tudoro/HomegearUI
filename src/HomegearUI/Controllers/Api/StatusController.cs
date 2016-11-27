using Microsoft.AspNetCore.Mvc;
using Abstractions.Models;
using Abstractions.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomegearUI.Controllers.Api
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private IHomegearConnectionService homegearService;
        public StatusController(IHomegearConnectionService homegearService)
        {
            this.homegearService = homegearService;
        }

        // GET: api/values
        [HttpGet]
        public HomegearStatusModel Get()
        {
            return homegearService.GetStatus();
        }
    }
}
