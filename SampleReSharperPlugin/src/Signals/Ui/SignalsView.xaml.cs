using JetBrains.Application.UI.Automation;

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
