using System;
using System.Collections.Generic;
using System.Text;

namespace JAVS.Hypnos.Shared.ServiceModels
{
    public class FaceDetectionStats
    {
        public bool IsZeroFaceAlert { get; set; }
        public List<FaceRect> FaceRectangles { get; set; }
    }
}
