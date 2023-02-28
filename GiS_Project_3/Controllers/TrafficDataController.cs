using Microsoft.AspNetCore.Mvc;
using GiS_Project_3.Models;
using GiS_Project_3.Repository;

namespace GiS_Project_3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrafficDataController : ControllerBase
    {
      
        private readonly ILogger<TrafficDataController> _logger;

        public TrafficDataController(ILogger<TrafficDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetTrafficData")]
        public IEnumerable<TrafficData> GetTrafficData()
        {
            var context = new NisContext();
            
                var records = context.NisFCDTable.Where(c=>c.Timestep==1);
                return records;

        }
    }
}