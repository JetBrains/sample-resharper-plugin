using JetBrains.UI.Wpf;

namespace SampleReSharperPlugin
{
    [View]
    public partial class ActionsView : IView<ActionsViewModel>
    {
        public new string Name => "Actions";

        public ActionsView()
        {
            InitializeComponent();
        }
    }
}
