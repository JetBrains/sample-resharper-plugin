using System.Windows.Media;
using JetBrains.Application.UI.Components.Theming;
using JetBrains.Application.UI.UIAutomation;
using JetBrains.DataFlow;

namespace SampleReSharperPlugin
{
    public class UiThemeViewModel : AAutomation
    {
        public IProperty<SolidColorBrush> BackgroundColor { get; set; }
        public IProperty<SolidColorBrush> TextColor { get; set; }

        public UiThemeViewModel(Lifetime lifetime, IColorThemeManager colorThemeManager)
        {
            BackgroundColor = new Property<SolidColorBrush>(lifetime, "UiThemeViewModel.BackgroundColor") {Value = new SolidColorBrush()};
            TextColor = new Property<SolidColorBrush>(lifetime, "UiThemeViewModel.TextColor") {Value = new SolidColorBrush()};

            var bgColor = colorThemeManager.CreateLiveColor(lifetime, ThemeColor.ToolWindowBackground);
            bgColor.ForEachValue(lifetime, (lt, color) => { BackgroundColor.Value.Color = color.WpfColor; });

            var txtColor = colorThemeManager.CreateLiveColor(lifetime, ThemeColor.ToolWindowForeground);
            txtColor.ForEachValue(lifetime, (lt, color) => { TextColor.Value.Color = color.WpfColor; });
        }
    }
}