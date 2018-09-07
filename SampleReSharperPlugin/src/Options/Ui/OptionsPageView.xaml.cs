using JetBrains.Application.UI.Automation;

namespace SampleReSharperPlugin
{
    [View]
    public partial class OptionsPageView : IView<OptionsPageViewModel>
    {
        public new string Name => "Options";

        public OptionsPageView()
        {
            InitializeComponent();
        }
    }
}
