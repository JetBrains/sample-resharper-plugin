using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using JetBrains.Application.UI.UIAutomation;
using JetBrains.DataFlow;
using JetBrains.ProjectModel;

namespace SampleReSharperPlugin
{
    public class PsiNavigationViewModel : AAutomation
    {
        public IProperty<string> NodeUnderCaretText { get; set; }
        public IProperty<IEnumerable<string>> ReferencedElementsNamesList { get; set; }
        public IProperty<int> SelectedReferencedElement { get; set; }

        public ICommand NavigateByReference => new NavigateByReference();
        public ICommand NavigateToParentMethod => new NavigateToParentMethod();        

        public PsiNavigationViewModel(Lifetime lifetime)
        {
            NodeUnderCaretText =
                new Property<string>(lifetime, "PsiNavigationViewModel.NodeUnderCaretProperty");

            ReferencedElementsNamesList =
                new Property<IEnumerable<string>>(lifetime, "PsiNavigationViewModel.ReferencedElementsNamesList")
                    {Value = new List<string> {"No references"}};


            var solutionStateTracker = SolutionStateTracker.Instance;

            solutionStateTracker?.SolutionName.Change.Advise_HasNew(lifetime, () =>
            {
                var solution = solutionStateTracker?.Solution;
                if (solution == null) return;

                var nodeUnderCaretDetector = solution.GetComponent<NodeUnderCaretDetector>();

                nodeUnderCaretDetector.NodeUnderCaret.FlowChangesInto(lifetime, NodeUnderCaretText, node =>
                {
                    var nodeText = node.GetText();
                    var nodeType = node.NodeType.ToString();
                    return $"{nodeType} : {nodeText}";
                });

                nodeUnderCaretDetector.NodeReferencedElements.FlowChangesInto(lifetime, ReferencedElementsNamesList,
                    elements =>
                    {
                        return elements?.Select(
                                       element => $"{element.GetElementType().PresentableName} : {element.ShortName}")
                                   .ToList() ?? new List<string> {"No references"};
                    });
            });
        }        
    }
}