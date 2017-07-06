using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace SampleReSharperPlugin
{
    [StaticSeverityHighlighting(Severity.WARNING, HighlightingGroupIds.GutterMarksGroup)]
    public class BadWordNamingWarning : IHighlighting
    {
        private readonly DocumentRange _documentRange;
        public readonly IVariableDeclaration VariableDeclaration;
        
        public BadWordNamingWarning(IVariableDeclaration variableDeclaration, DocumentRange documentRange)
        {
            VariableDeclaration = variableDeclaration;
            _documentRange = documentRange;
        }

        public bool IsValid()
        {
            return VariableDeclaration.IsValid();
        }

        public DocumentRange CalculateRange()
        {
            return _documentRange;
        }

        public string ToolTip => "The name contains a bad word";
        public string ErrorStripeToolTip { get; }
    }
}