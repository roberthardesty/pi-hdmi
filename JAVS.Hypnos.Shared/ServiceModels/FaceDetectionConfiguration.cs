using System;
using System.Collections.Generic;
using System.Text;

namespace JAVS.Hypnos.Shared.ServiceModels
{
    public class FaceDetectionConfiguration
    {
        public double ScaleFactor { get; set; }
        public int MinimumNeighbors { get; set; }
        public int MinimumFaceWidth { get; set; }
        public int MinimumFaceHeight { get; set; }
        public double FaceTimeoutInSeconds { get; set; }
    }
}
