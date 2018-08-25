using System;

namespace JAVS.Hypnos.Pi.Core
{
    public class PiConfiguration
    {
        public static string DevSignalRAddress { get; set; } = "http://192.168.1.68:5005";
        public static string ProdSignalRAddress { get; set; }
    }
}
