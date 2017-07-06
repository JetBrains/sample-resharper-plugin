using System;
using System.Windows.Input;
using JetBrains.ActionManagement;
using JetBrains.Annotations;
using JetBrains.Application;
using JetBrains.DataFlow;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.Threading;
using JetBrains.UI.ActionsRevised;

namespace SampleReSharperPlugin
{
    public class RunActionCommand : ICommand
    {
        private readonly Lifetime _lifetime;
        private readonly Type _actionType;

        public event EventHandler CanExecuteChanged;

        public RunActionCommand(Lifetime lifetime, [NotNull] Type actionType)
        {
            _lifetime = lifetime;                
            _actionType = actionType;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }


        public void ExecuteCommand<T>() where T : IExecutableAction
        {            
            var actionManager = Shell.Instance.GetComponent<IActionManager>();
            actionManager.ExecuteActionAsync<T>(_lifetime);           

            var shellLocks = Shell.Instance.GetComponent<IShellLocks>();
            shellLocks.ExecuteOrQueue(_lifetime, "ExecuteAction", () =>
            {
                actionManager.ExecuteAction<ShowMessageBoxAction>();
            });

        }


        public void Execute(object parameter)
        {
            var method = typeof(RunActionCommand).GetMethod("ExecuteCommand");
            var genMethod = method.MakeGenericMethod(_actionType);
            genMethod.Invoke(this, null);
        }
    }
}