using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using signalr_server.HubConfig;
using signalr_server.TimeFeatures;

namespace signalr_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private IHubContext<ChartHub> _hub;

        public ChartController(IHubContext<ChartHub> hub)
        {
            _hub = hub;
        }

        // [HttpGet]
        // public IActionResult Get()
        // {
        //     _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData());
        //     var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData()));

        //     return Ok(new { Message = "Request Completed" });
        // }
    }
}