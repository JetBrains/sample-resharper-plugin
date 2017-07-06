using JetBrains.ActionManagement;
using JetBrains.Application;
using JetBrains.Application.DataContext;
using JetBrains.Application.Settings.Store.Implementation;
using JetBrains.DataFlow;
using JetBrains.UI.ActionsRevised;
using JetBrains.UI.Application;
using JetBrains.UI.Components.Theming;
using JetBrains.UI.MenuGroups;
using JetBrains.UI.ToolWindowManagement;

namespace SampleReSharperPlugin
{
    [Action("ActionOpenToolWindow", "Show sample tool window...", Id = 543211)]
    public class OpenToolWindowAction : SampleAction, IInsertLast<MainMenuFeaturesGroup>
    {
        protected override void RunAction(IDataContext context, DelegateExecute nextExecute)
        {
            var lifetime = context.GetComponent<Lifetime>();
            var toolWindowManager = context.GetComponent<ToolWindowManager>();
            var toolWindowDescriptor = context.GetComponent<SampleToolWindowDescriptor>();
            var settingsStore = context.GetComponent<SettingsStore>();
            var colorThemeManager = context.GetComponent<IColorThemeManager>();
            ToolWindowInstance[] instances = null;

            foreach (var @class in toolWindowManager.Classes.Classes)
            {
                if (@class.Descriptor.Id.ProductNeutralId == "MyToolWindow")
                {
                    instances = @class.Instances;
                }
            }

            if (instances == null) return;

            if (instances.Length == 0)
            {
                var toolWindow = new SampleToolWindow(lifetime, toolWindowManager, toolWindowDescriptor, settingsStore,
                    colorThemeManager);
                toolWindow.Show();
            }
            else
                // we have always only one instance
                instances[0].Show();
        }
    }
}