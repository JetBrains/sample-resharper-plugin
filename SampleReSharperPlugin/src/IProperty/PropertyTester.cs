using JetBrains.DataFlow;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    public class PropertyTester
    {
        private readonly PropertyProvider _propertyProvider;

        public PropertyTester(Lifetime lifetime, PropertyProvider propertyProvider)
        {
            _propertyProvider = propertyProvider;
            _propertyProvider.SomeIntProperty.BeforeChange.Advise(lifetime, args =>
            {
                if (args.New < 0)
                    args.Cancel = true;
            });

            _propertyProvider.SomeIntProperty.Change.Advise_HasNew(lifetime,
                val => MessageBox.ShowInfo($"New property value is {val.New}"));           
        }

        public void ChangePropertyValue(int value)
        {
            _propertyProvider.SomeIntProperty.Value = value;
        }
    }    
}