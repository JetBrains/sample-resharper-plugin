using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.Application.Threading;
using JetBrains.DataFlow;
using JetBrains.DocumentManagers;
using JetBrains.DocumentManagers.Transactions;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Navigation;
using JetBrains.ReSharper.Feature.Services.Navigation.NavigationExtensions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    [SolutionComponent]
    public class NodeUnderCaretDetector
    {
        public ISolution Solution { get; }
        private readonly Lifetime _lifetime;
        private readonly DocumentManager _documentManager;
        private readonly ITextControlManager _textControlManager;
        private readonly IShellLocks _shellLocks;
        public IProperty<ITreeNode> NodeUnderCaret { get; set; }
        public IProperty<IEnumerable<IDeclaredElement>> NodeReferencedElements { get; set; }


        public NodeUnderCaretDetector(Lifetime lifetime, ISolution solution,
            DocumentManager documentManager,
            ITextControlManager textControlManager, IShellLocks shellLocks)


        {
            Solution = solution;
            _lifetime = lifetime;
            _documentManager = documentManager;
            _textControlManager = textControlManager;
            _shellLocks = shellLocks;
            NodeUnderCaret = new Property<ITreeNode>("NodeUnderCaretDetector.NodeUnderCaret");
            NodeReferencedElements =
                new Property<IEnumerable<IDeclaredElement>>("NodeUnderCaretDetector.NodeReferencedElements");

            EventHandler caretMoved = (sender, args) =>
            {
                _shellLocks.QueueReadLock("NodeUnderCaretDetector.CaretMoved", Refresh);
            };

            lifetime.AddBracket(
                () => _textControlManager.Legacy.CaretMoved += caretMoved,
                () => _textControlManager.Legacy.CaretMoved -= caretMoved);
        }


        public void NavigateToParentMethod()
        {
            _shellLocks.ExecuteOrQueueReadLock(_lifetime, "NavigateToParent", () =>
            {
                var node = NodeUnderCaret.Value;
                var parentMethod = node.GetParentOfType<IMethodDeclaration>();
                parentMethod?.NavigateToTreeNode(true);
            });
        }
        

        public void NavigateToFirstReferencedElement()
        {
            _shellLocks.ExecuteOrQueueReadLock(_lifetime, "NavigateByReference", () =>
            {
                var element = NodeReferencedElements.Value?.FirstNotNull();
                element?.Navigate(true);
            });
        }


        public void Refresh()
        {
            var node = GetTreeNodeUnderCaret();
            NodeUnderCaret.Value = node;
            NodeReferencedElements.Value = node.GetReferencedElements();
        }


        [CanBeNull]
        private IProjectFile GetProjectFile([CanBeNull] ITextControl textControl)
        {
            return textControl == null ? null : _documentManager.TryGetProjectFile(textControl.Document);
        }


        [CanBeNull]
        public ITreeNode GetTreeNodeUnderCaret()
        {
            var textControl = _textControlManager.LastFocusedTextControl.Value;
            if (textControl == null)
                return null;

            var projectFile = _documentManager.TryGetProjectFile(textControl.Document);
            if (projectFile == null)
                return null;

            var range = new TextRange(textControl.Caret.Offset());

            var psiSourceFile = projectFile.ToSourceFile().NotNull("File is null");            

            var documentRange = range.CreateDocumentRange(projectFile);
            var file = psiSourceFile.GetPsiFile(psiSourceFile.PrimaryPsiLanguage, documentRange);

            var element = file?.FindNodeAt(documentRange);

            return element;
        }
    }
}