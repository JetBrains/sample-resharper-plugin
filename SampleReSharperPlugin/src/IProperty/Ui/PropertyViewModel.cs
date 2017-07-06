using JetBrains.DataFlow;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.UI.Wpf;

namespace SampleReSharperPlugin
{
    public class PropertyViewModel : AAutomation
    {
        public IProperty<int> SomeIntProperty { get; set; }
        public IProperty<string> SourceProperty { get; set; }
        public IProperty<string> TargetProperty { get; set; }


        public PropertyViewModel(Lifetime lifetime)
        {
            // SomeIntProperty of the PropertyProvider instance is updated via the SomeIntProperty of PropertyViewModel
            // which, in turn, is updated from PropertyView via WPF binding
            SomeIntProperty = new Property<int>(lifetime, "PropertyViewModel.SomeIntProperty") {Value = 0};
            
            var propProvider = new PropertyProvider(lifetime);

            SomeIntProperty.FlowInto(lifetime, propProvider.SomeIntProperty);
            
            var propTester = new PropertyTester(lifetime, propProvider);


            // IProperty FlowChangesInto example
            SourceProperty = new Property<string>(lifetime, "PropertyViewModel.SourceProperty");
            TargetProperty = new Property<string>(lifetime, "PropertyViewModel.TargetProperty");
            var propFlow = new PropertyFlow(lifetime);

            SourceProperty.FlowInto(lifetime, propFlow.SourceProperty);
            propFlow.TargetProperty.FlowInto(lifetime, TargetProperty);
        }
    }
}