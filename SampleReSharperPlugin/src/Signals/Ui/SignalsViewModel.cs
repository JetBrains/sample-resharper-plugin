using JetBrains.DataFlow;
using JetBrains.UI.Wpf;

namespace SampleReSharperPlugin
{
    public class SignalsViewModel : AAutomation
    {
        public IProperty<bool> IsChecked { get; set; }

        public SignalsViewModel(Lifetime lifetime)
        {
            IsChecked = new Property<bool>(lifetime, "SignalsViewModel.IsChecked") {Value = false};

            var signalEmitter = new SignalEmitter(lifetime);

            IsChecked.Change.Advise_HasNew(lifetime,
                val => { signalEmitter.MakeItHappen(val.New ? "Checked" : "Unchecked"); });

            var signalListener = new SignalListener(lifetime, signalEmitter);            
        }
    }
}