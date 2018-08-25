using JAVS.Hypnos.Shared.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Shared.Services
{
    public interface IFaceDetectionListener
    {
        void ListenFor<T>(Action<T> handler);
        Task<SignalRServerResponse> UpdateFaceDetectionConfig();
        Task<SignalRServerResponse> Join(JoinGroupRequest request);
    }
}
