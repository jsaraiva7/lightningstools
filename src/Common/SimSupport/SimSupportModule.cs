using System;
using System.Collections.Generic;

namespace Common.SimSupport
{
    public abstract class SimSupportModule : IDisposable
    {
        public abstract string FriendlyName { get; }
        public abstract bool IsSimRunning { get; }
        public abstract Dictionary<string, SimCommand> SimCommands { get; }
        public abstract Dictionary<string, ISimOutput> SimOutputs { get; }
        public bool TestMode { get; set; }

        public abstract void Dispose();
        public abstract void Update();

        //Method to call UI to configure SSM defs - Jsa 12/04/2019
        public abstract void Configure();

    }
}