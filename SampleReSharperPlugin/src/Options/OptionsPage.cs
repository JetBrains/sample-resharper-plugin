using JetBrains.Annotations;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Feature.Services.Resources;
using JetBrains.UI.Options;
using JetBrains.UI.Options.OptionPages.ToolsPages;
using JetBrains.UI.Options.OptionsDialog2.SimpleOptions;

namespace SampleReSharperPlugin
{
    [OptionsPage(Pid, "Sample R# Plugin", typeof(FeaturesEnvironmentOptionsThemedIcons.CodeInspections),
        ParentId = ToolsPage.PID)]
    public class OptionsPage : SimpleOptionsPage
    {
        private const string Pid = "MyPluginOptions";

        public OptionsPage([NotNull] Lifetime lifetime,
            [NotNull] OptionsSettingsSmartContext optionsSettingsSmartContext)
            : base(lifetime, optionsSettingsSmartContext)
        {
            IProperty<bool> checkMe = new Property<bool>(lifetime, "MyOptionsPage::SomeOption");
            checkMe.SetValue(
                optionsSettingsSmartContext.StoreOptionsTransactionContext.GetValue(
                    (MySettingsKey key) => key.CheckMe));

            checkMe.Change.Advise(lifetime, a =>
            {
                if (!a.HasNew) return;
                optionsSettingsSmartContext.StoreOptionsTransactionContext.SetValue(
                    (MySettingsKey key) => key.CheckMe, a.New);
            });

            AddBoolOption((MySettingsKey key) => key.CheckMe, "Sample bool option");
        }
    }

    [SettingsKey(typeof(EnvironmentSettings), "My settings")]
    public class MySettingsKey
    {
        [SettingsEntry(false, "Check Me")]
        public bool CheckMe;
    }
}