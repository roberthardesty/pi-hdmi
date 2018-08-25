using System;
using System.Collections.Generic;
using System.Text;

namespace JAVS.Hypnos.Shared.ServiceModels
{
    public class ClientGroupStats: SignalRServerResponse
    {
        public string GroupName { get; set; }
        public Dictionary<string, int> ConnectedClientGroupCounts { get; set; }
    }
}
