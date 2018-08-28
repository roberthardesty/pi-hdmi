using JAVS.Hypnos.Pi.Core;
using JAVS.Hypnos.Shared;
using JAVS.Hypnos.Shared.ServiceModels;
using JAVS.Hypnos.Shared.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Pi.HDMIController.Services
{
    public class FaceDetectionService : ServiceClient, IFaceDetectionListener
    {
        private HubConnection _connection;
        public FaceDetectionService() : base(HubNames.FaceDetection)
        {

        }

        public async Task Init()
        {
            _connection = GetHubConnection();
        }

        public void ListenFor<T>(Action<T> handler)
        {
            _connection.On(typeof(T).Name, handler);
        }

        public async Task<SignalRServerResponse> Join(JoinGroupRequest request)
        {
            await _connection.StartAsync();

            return await MutableCallSameMethodOnTheHub<SignalRServerResponse>(_connection, new object[] { request });
        }

        public Task<SignalRServerResponse> UpdateFaceDetectionConfig()
        {
            throw new NotImplementedException();
        }
    }

}
