using System;
using System.Linq;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.Refactorings.Specific.Rename;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    [ContextAction(Name = "ToLowerCase", Description = "Convert the text to lowercase", Group = "C#", Disabled = false,
        Priority = 1)]
    public class ToLowerCaseContextAction : ContextActionBase
    {
        private readonly IVariableDeclaration _nodeUnderCaret;

        public ToLowerCaseContextAction(LanguageIndependentContextActionDataProvider dataProvider)
        {
            _nodeUnderCaret = dataProvider.GetSelectedElement<IVariableDeclaration>();            
        }


        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            return textControl =>
            {
                var newText = _nodeUnderCaret.DeclaredName.ToLower();
                RenameRefactoringService.Rename(solution,
                    new RenameDataProvider((IDeclaredElement) _nodeUnderCaret, newText), textControl);
            };
        }

        public override string Text => "Convert to lowercase";

        public override bool IsAvailable(IUserDataHolder cache)
        {
            var nodeText = _nodeUnderCaret?.DeclaredName;
            var containsUpperCase = nodeText != null && nodeText.Any(char.IsUpper);
            return containsUpperCase;
        }
    }
}