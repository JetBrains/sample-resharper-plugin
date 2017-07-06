using System;
using System.Windows.Input;
using JetBrains.ProjectModel;

namespace SampleReSharperPlugin
{
    public class NavigateToParentMethod : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var solutionStateTracker = SolutionStateTracker.Instance;
            var solution = solutionStateTracker.Solution;
            var detector = solution?.GetComponent<NodeUnderCaretDetector>();
            detector?.NavigateToParentMethod();
        }

        public event EventHandler CanExecuteChanged;
    }
}