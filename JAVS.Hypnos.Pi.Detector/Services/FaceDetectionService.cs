using JAVS.Hypnos.Pi.Core;
using JAVS.Hypnos.Shared;
using JAVS.Hypnos.Shared.ServiceModels;
using JAVS.Hypnos.Shared.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Pi.Detector.Services
{
    public class FaceDetectionService : ServiceClient, IFaceDetectionService, IFaceDetectionListener
    {
        private HubConnection _connection;
        public FaceDetectionService() : base(HubNames.FaceDetection)
        {

        }

        public async Task Init()
        {
            _connection = GetHubConnection();
            await _connection.StartAsync();
        }

        public void ListenFor<T>(Action<T> handler)
        {
            _connection.On(nameof(T), handler);
        }

        public async Task<SignalRServerResponse> Join(JoinGroupRequest request)
        {
            return await MutableCallSameMethodOnTheHub<SignalRServerResponse>(_connection, new object[] { request });
        }

        public async Task<SignalRServerResponse> PublishFaceDetectionStats(FaceDetectionStats stats)
        {
            return await MutableCallSameMethodOnTheHub<SignalRServerResponse>(_connection, new object[] { stats });
        }

        public Task<SignalRServerResponse> UpdateFaceDetectionConfig()
        {
            throw new NotImplementedException();
        }
    }
}
