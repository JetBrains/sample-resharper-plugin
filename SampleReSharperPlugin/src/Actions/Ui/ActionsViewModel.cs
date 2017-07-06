using System.Windows.Input;
using JetBrains.DataFlow;
using JetBrains.UI.Wpf;

namespace SampleReSharperPlugin
{
    public class ActionsViewModel: AAutomation
    {
        private readonly Lifetime _lifetime;        

        public ActionsViewModel(Lifetime lifetime)
        {
            _lifetime = lifetime;
        }

        public ICommand RunAction => new RunActionCommand(_lifetime, typeof(ShowMessageBoxAction));
    }
}