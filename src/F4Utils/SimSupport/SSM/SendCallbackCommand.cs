using Common.SimSupport;
using F4Utils.Process;
using GlobalConfiguration;

namespace F4Utils.SimSupport.SSM
{
    public class SendCallbackCommand : SimCommand
    {
        public string Callback { get; set; }

        public override void Execute()
        {
            var cfg = F4Config.GetConfig();
            KeyFileUtils.SendCallbackToFalcon(Callback, cfg.F4ExeFile);
        }
    }
}