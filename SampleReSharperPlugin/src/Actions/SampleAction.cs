using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.UI.ActionsRevised;

namespace SampleReSharperPlugin
{
    public abstract class SampleAction : IExecutableAction
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            return true; // function result indicates whether the menu item is enabled or disabled
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            RunAction(context, nextExecute);
        }

        protected abstract void RunAction(IDataContext context, DelegateExecute nextExecute);
    }
}