using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace JAVS.Hypnos.Service.Hubs
{
    public class SecondHub: Hub
    {
        public Task Fire(string message) {
            return Clients.All.SendAsync("Fire",  $"Firing from second Hub {message}");
        }
    }
}