using System;

namespace JAVS.Hypnos.Pi.Core
{
    public class PiSettings
    {
        public string SignalRAddress { get; set; } = "http://192.168.1.68:5005";
        public virtual string PiType { get; set; }
    }
}
