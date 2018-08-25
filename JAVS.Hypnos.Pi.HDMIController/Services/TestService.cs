using JAVS.Hypnos.Pi.Core;
using JAVS.Hypnos.Shared.ServiceModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Pi.HDMIController.Services
{
    public class TestService : ServiceClient
    {
        private HubConnection _connection;
        public TestService() : base("testHub")
        {
        }

        public async Task Init()
        {
            _connection = GetHubConnection();
            await _connection.StartAsync();
        }

        public void Listen(Action<string> reaction)
        {
            _connection.On(nameof(Listen), reaction);
        }

        public async Task<SignalRServerResponse> Talk(string words)
        {
            return await MutableCallSameMethodOnTheHub<SignalRServerResponse>(_connection, new object[] { words });
        }
    }
}
