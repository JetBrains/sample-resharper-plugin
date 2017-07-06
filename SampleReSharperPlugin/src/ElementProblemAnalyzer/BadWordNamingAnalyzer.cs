using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;


namespace SampleReSharperPlugin
{
    [ElementProblemAnalyzer(typeof(IVariableDeclaration), HighlightingTypes =
        new[] {typeof(BadWordNamingWarning)})]
    public class BadWordNamingAnalyzer : ElementProblemAnalyzer<IVariableDeclaration>
    {
        protected override void Run(IVariableDeclaration element, ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer)
        {
            var nodeText = element.DeclaredName.ToLower();            

            if (!nodeText.Contains("crap"))
                return;

            consumer.AddHighlighting(new BadWordNamingWarning(element, element.NameIdentifier.GetDocumentRange()));
        }
    }
}