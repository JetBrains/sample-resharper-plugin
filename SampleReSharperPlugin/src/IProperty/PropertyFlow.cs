using System.Globalization;
using JetBrains.DataFlow;

namespace SampleReSharperPlugin
{
    public class PropertyFlow
    {
        public IProperty<string> SourceProperty { get; set; }
        public IProperty<string> TargetProperty { get; set; }

        public PropertyFlow(Lifetime lifetime)
        {
            SourceProperty = new Property<string>(lifetime, "sourceProperty");
            TargetProperty = new Property<string>(lifetime, "targetProperty");

            SourceProperty.FlowChangesInto(lifetime, TargetProperty, s =>
            {
                if (string.IsNullOrEmpty(s))
                    return "";

                var textInfo = new CultureInfo("en-US", false).TextInfo;
                s = textInfo.ToTitleCase(s); 

                return s;
            });
        }
    }
}