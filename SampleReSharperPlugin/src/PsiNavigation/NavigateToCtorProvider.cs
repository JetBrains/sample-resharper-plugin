using System.Collections.Generic;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Feature.Services.Navigation.ContextNavigation;
using JetBrains.ReSharper.Feature.Services.Navigation.NavigationExtensions;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace SampleReSharperPlugin
{

    [ContextNavigationProvider]
    public class NavigateToCtorProvider : INavigateFromHereProvider
    {
        public IEnumerable<ContextNavigation> CreateWorkflow(IDataContext dataContext)
        {
            var node = dataContext.GetSelectedTreeNode<ITreeNode>();
            var typeDeclaration = node?.GetParentOfType<IClassDeclaration>();            
            var constructor = typeDeclaration?.ConstructorDeclarations.FirstNotNull();

            if (constructor != null)
            {
                yield return new ContextNavigation("Constructor", null, NavigationActionGroup.Other, () =>
                {
                    constructor.NavigateToTreeNode(true);
                });
            }                       
        }
    }
}