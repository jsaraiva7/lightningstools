using System;
using Common.MacroProgramming;

namespace Common.SimLinkup.Signals
{
    [Serializable]
    public class SignalMapping
    {
        public Signal Destination { get; set; }
        public Signal Source { get; set; }
    }
}