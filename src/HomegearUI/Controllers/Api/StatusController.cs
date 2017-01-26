using Microsoft.AspNetCore.Mvc;
using Abstractions.Models;
using Abstractions.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HomegearUI.Controllers.Api
{
    /// <summary>
    /// Manages the status of the connection to the Homegear server
    /// </summary>
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private IHomegearConnectionService _homegearService;
        public StatusController(IHomegearConnectionService homegearService)
        {
            this._homegearService = homegearService;
        }
        
        /// <summary>
        /// Gets the current status of the connection to the Homegear server
        /// </summary>
        /// <returns>True if it is connected, False otherwise</returns>
        // GET: api/values
        [HttpGet]
        public HomegearStatusModel Get()
        {
            return _homegearService.GetStatus();
        }
    }
}
