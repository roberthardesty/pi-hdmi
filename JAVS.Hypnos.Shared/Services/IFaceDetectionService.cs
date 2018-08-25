using JAVS.Hypnos.Shared.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JAVS.Hypnos.Shared.Services
{
    public interface IFaceDetectionService
    {
        Task<SignalRServerResponse> PublishFaceDetectionStats(FaceDetectionStats stats);
    }
}
