using JetBrains.Application;
using JetBrains.Application.UI.ToolWindowManagement;

namespace SampleReSharperPlugin
{    
    [ToolWindowDescriptor(
            ProductNeutralId = "MyToolWindow",
            Text = "Sample Tool Window",
            Type = ToolWindowType.MultiInstance,
            Icon = typeof(JetBrains.UI.Resources.ExtensionManagerThemedIcons.ExtensionInstall),
            VisibilityPersistenceScope = ToolWindowVisibilityPersistenceScope.Global,
            InitialDocking = ToolWindowInitialDocking.Right
        )
    ]
    public class SampleToolWindowDescriptor : ToolWindowDescriptor
    {
        public SampleToolWindowDescriptor(IApplicationHost host) : base(host)
        {
        }
    }

}