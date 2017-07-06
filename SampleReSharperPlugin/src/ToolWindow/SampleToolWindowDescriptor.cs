using JetBrains.Application;
using JetBrains.UI.ToolWindowManagement;

namespace SampleReSharperPlugin
{    
    [ToolWindowDescriptor(
            ProductNeutralId = "MyToolWindow",
            Text = "Sample Tool Window",
            Icon = typeof(JetBrains.Ide.Resources.IdeThemedIcons.TextDocument),
            Type = ToolWindowType.MultiInstance,
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