using System.Threading.Tasks;
using JAVS.Hypnos.Service.Hubs;
using JAVS.Hypnos.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace JAVS.Hypnos.Service.Controllers
{

    [ApiController]
    [Route("/message")]
    public class MessageController : Controller
    {
        public IHubContext<ApplicationHub> _hubContext { get; }

        public MessageController(IHubContext<ApplicationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public Task PostMessage(ChatMessage message)
        {
            return _hubContext.Clients.All.SendAsync("Send", message.Message);
        }
    }
}