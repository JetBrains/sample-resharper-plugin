using JetBrains.DataFlow;

namespace SampleReSharperPlugin
{
    public class SignalEmitter
    {
        public ISignal<string> SomethingHappened;

        public SignalEmitter(Lifetime lifetime)
        {
            SomethingHappened = new Signal<string>(lifetime, "SignalEmitter.SomethingHappened");
        }

        public void MakeItHappen(string arg)
        {
            SomethingHappened.Fire(arg);
        }
    }
}