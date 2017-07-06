using JetBrains.Application;
using JetBrains.DataFlow;
using JetBrains.UI.Wpf;

namespace SampleReSharperPlugin
{
    public class PropertyProvider
    {
        public IProperty<int> SomeIntProperty { get; set; }

        public PropertyProvider(Lifetime lifetime)
        {
            SomeIntProperty = new Property<int>(lifetime, "PropertyProvider.SomeIntProperty") { Value = 0 };
        }
    }
}