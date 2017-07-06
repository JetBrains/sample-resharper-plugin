using JetBrains.DataFlow;
using JetBrains.ProjectModel;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    [SolutionComponent]
    public class SampleSolutionComponent
    {
        // You can demand almost any ReSharper components in the constructor
        public SampleSolutionComponent(Lifetime lifetime, ISolutionStateTracker solutionStateTracker)
        {
            solutionStateTracker.AfterSolutionOpened.Advise(lifetime,
                () => { MessageBox.ShowInfo("Finished loading the solution"); });
        }
    }
}