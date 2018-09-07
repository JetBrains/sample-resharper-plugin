using JetBrains.Application.UI.Automation;

namespace SampleReSharperPlugin
{    
    [View]
    public partial class UiThemeView : IView<UiThemeViewModel>
    {
        public new string Name => "Color Themes";
    
        public UiThemeView()
        {
            InitializeComponent();
        }
    }
}