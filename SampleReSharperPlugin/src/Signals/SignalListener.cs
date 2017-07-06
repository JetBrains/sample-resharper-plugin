using JetBrains.DataFlow;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    public class SignalListener
    {
        public SignalListener(Lifetime lifetime, SignalEmitter signalEmitter)
        {
            signalEmitter.SomethingHappened.Advise(lifetime,
                arg => MessageBox.ShowInfo($"{arg}"));            
        }
    }
}