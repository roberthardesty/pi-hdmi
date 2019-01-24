using System;
using System.Collections.Generic;
using System.Text;

namespace JAVS.Hypnos.Pi.Detector
{
    public class PiSettings: Core.PiSettings
    {
        public override string PiType { get => base.PiType ?? "Dectector"; set => base.PiType = value; }
        public string LinuxPathToFaceHaarCascade { get; set; } = @"/share/JAVS.Hypnos.Pi.Detector/Data/haarcascade_frontalface_alt.xml";
        public string DevPathToFaceHaarCascade { get; set; } = @"./Data/haarcascade_frontalface_alt.xml";
    }
}
