using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.UI.ActionsRevised;
using JetBrains.UI.MenuGroups;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    [Action("ActionShowMessageBox", "Show message box", Id = 543210)]
    public class ShowMessageBoxAction : SampleAction
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