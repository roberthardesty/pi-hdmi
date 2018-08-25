using JAVS.Hypnos.Shared.ServiceModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Service.Hubs
{
    public class TestHub: Hub
    {
        public string TestConnection()
        {
            return "You are connected";
        }
        
        public async Task<SignalRServerResponse> Talk(string message)
        {
            await Clients.Others.SendAsync("Listen", message);

            return new SignalRServerResponse()
            {
                Success = true
            };
        }
    }
}
