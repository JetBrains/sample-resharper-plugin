using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Feature.Services.Refactorings.Specific.Rename;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    [QuickFix]
    public class CorrectBadWordQuickFix : QuickFixBase
    {
        private readonly IVariableDeclaration _variableDeclaration;

        public CorrectBadWordQuickFix([NotNull] BadWordNamingWarning warning)
        {
            _variableDeclaration = warning.VariableDeclaration;
        }

        protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
        {
            return textControl =>
            {
                var newText = Regex.Replace(_variableDeclaration.DeclaredName, "crap", "BadWord", RegexOptions.IgnoreCase);
                
                RenameRefactoringService.Rename(solution,
                    new RenameDataProvider(_variableDeclaration.DeclaredElement, newText), textControl);
            };
        }

        public override string Text => "Replace the bad word";

        public override bool IsAvailable(IUserDataHolder cache)
        {
            return _variableDeclaration.IsValid();
        }
    }
}
