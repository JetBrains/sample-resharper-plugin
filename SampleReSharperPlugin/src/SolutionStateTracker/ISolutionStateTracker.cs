using JetBrains.Annotations;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;

namespace SampleReSharperPlugin
{
    public interface ISolutionStateTracker
    {
        [CanBeNull]
        ISolution Solution { get; }
        ISignal<ISolution> AfterSolutionOpened { get; }
        ISignal<ISolution> BeforeSolutionClosed { get; }
    }
}