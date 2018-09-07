using JetBrains.Application.DataContext;
using JetBrains.Application.UI.Actions;
using JetBrains.Application.UI.ActionsRevised.Menu;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    [Action("ActionShowMessageBox", "Show message box", Id = 543210)]
    public class ShowMessageBoxAction : SampleAction, IExecutableAction
    {
        protected override void RunAction(IDataContext context, DelegateExecute nextExecute)
        {
            var solution = context.GetData(JetBrains.ProjectModel.DataContext.ProjectModelDataConstants.SOLUTION);
            MessageBox.ShowInfo(solution?.SolutionFile != null
                ? $"{solution.SolutionFile?.Name} solution is opened"
                : "No solution is opened");
        }
    }
}