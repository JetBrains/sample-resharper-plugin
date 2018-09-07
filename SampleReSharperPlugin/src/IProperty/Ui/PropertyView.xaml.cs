using JetBrains.Application.UI.Automation;

namespace SampleReSharperPlugin
{
    public partial class PropertyView : IView<PropertyViewModel>
    {
        public new string Name => "IProperty";

        public PropertyView()
        {
            InitializeComponent();
        }
    }
}