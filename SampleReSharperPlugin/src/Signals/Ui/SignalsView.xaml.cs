using JetBrains.DataFlow;
using JetBrains.ReSharper.Psi;
using JetBrains.UI.Wpf;

namespace SampleReSharperPlugin
{    
    public partial class SignalsView : IView<SignalsViewModel>    
    {
        public new string Name => "Signals";     

        public SignalsView()
        {
            InitializeComponent();
        }
    }
}
