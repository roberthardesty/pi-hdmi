using JAVS.Hypnos.Shared.ServiceModels;
using JAVS.Hypnos.Shared.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Service.Hubs
{
    public class FaceDetectionHub: Hub, IFaceDetectionService
    {
        private const string CONTROL_GROUP = "CONTROL_GROUP";
        private const string SPECTATOR_GROUP = "SPECTATOR_GROUP";
        private const string DETECTOR_GROUP = "DETECTOR_GROUP";
        
        private static Dictionary<string, string> _clientIDGroupNameHash = new Dictionary<string,string>();

        public FaceDetectionHub()
        {
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            _clientIDGroupNameHash.Remove(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _clientIDGroupNameHash[Context.ConnectionId]);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task<SignalRServerResponse> UpdateFaceDetectionConfig()
        {
            if (_clientIDGroupNameHash[Context.ConnectionId] != CONTROL_GROUP)
                return new SignalRServerResponse()
                {
                    Success = false,
                    Error = new Exception("Nice try.")
                };

            var consumerClientIDs = _clientIDGroupNameHash.Where(cg => cg.Value == CONTROL_GROUP || cg.Value == SPECTATOR_GROUP || cg.Value == DETECTOR_GROUP)
                                                          .Select(cg => cg.Key)
                                                          .ToList();

            await Clients.Clients(consumerClientIDs).SendAsync("");

            return new SignalRServerResponse()
            {
                Success = true
            };
        }

        public async Task<SignalRServerResponse> PublishFaceDetectionStats(FaceDetectionStats stats)
        {
            await Clients.Groups(new List<string>{SPECTATOR_GROUP, CONTROL_GROUP}).SendAsync(nameof(FaceDetectionStats), stats);

            return new SignalRServerResponse()
            {
                Success = true
            };
        }

        public async Task<SignalRServerResponse> Join(JoinGroupRequest request)
        {
            if(request.IsDetector)
                await AddToDetectorGroup();
            else
                await AddToConsumerGroup(request.IsHDMIController);
            // Here we are selecting all the IDs that are currently in the ClientID - GroupName hash and putting them in a list
            IReadOnlyList<string> joinedClients = _clientIDGroupNameHash.Select(cg => cg.Key)
                                                                        .Where(id => id != Context.ConnectionId)
                                                                        .ToList();
            // build the counts of each group
            ClientGroupStats otherGroupStats = BuildClientGroupStatsResponse();
            // this one contains the group that was just joined
            ClientGroupStats senderGroupStats = BuildClientGroupStatsResponse(true);
            // publish to all clients that have joined a group that there are new group stats.
            await Clients.Clients(joinedClients).SendAsync(nameof(ClientGroupStats), otherGroupStats);
            await Clients.Caller.SendAsync(nameof(ClientGroupStats), senderGroupStats);

            return new SignalRServerResponse() { Success = true };
        }

        private async Task AddToConsumerGroup(bool isHDMIController)
        {
            int controllerClients = _clientIDGroupNameHash.Where(cg => cg.Value == CONTROL_GROUP).Count();
            if(controllerClients < 2 && !isHDMIController) // Only allow two web clients to control the detector at the same time
            {
                _clientIDGroupNameHash.Add(Context.ConnectionId, CONTROL_GROUP);
            }
            else
            {
                _clientIDGroupNameHash.Add(Context.ConnectionId, SPECTATOR_GROUP);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, _clientIDGroupNameHash[Context.ConnectionId]);
        }

        private async Task AddToDetectorGroup()
        {
            _clientIDGroupNameHash.Add(Context.ConnectionId, DETECTOR_GROUP);
            await Groups.AddToGroupAsync(Context.ConnectionId, _clientIDGroupNameHash[Context.ConnectionId]);
        }

        private ClientGroupStats BuildClientGroupStatsResponse(bool forSender = false)
        {
             Dictionary<string, int> clientGroupCounts = _clientIDGroupNameHash.GroupBy(x => x.Value)
                                                                              .ToDictionary(x => x.Key, x => x.Count());
            return new ClientGroupStats()
            {
                GroupName = forSender ? _clientIDGroupNameHash[Context.ConnectionId] : "",
                ConnectedClientGroupCounts = clientGroupCounts
            };
        }

    }
}
