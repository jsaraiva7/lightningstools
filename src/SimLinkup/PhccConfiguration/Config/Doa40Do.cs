using System;

namespace PhccConfiguration.Config
{
    [Serializable]
    public class Doa40Do : Peripheral
    {
        public override string ToString()
        {
            return "Doa40Do - " + FriendlyName + " - " + Address.ToString("X");

        }
    }
}