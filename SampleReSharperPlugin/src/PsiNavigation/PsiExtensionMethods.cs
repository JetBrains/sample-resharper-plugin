using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JetBrains.Application.Threading;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Navigation.NavigationExtensions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Files;
using JetBrains.ReSharper.Psi.Paths;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    public static class PsiExtensionMethods
    {
        [CanBeNull]
        public static IProject GetProjectByName(this ISolution solution, string projectName)
        {
            var projects = solution.GetTopLevelProjects();
            return projects.FirstOrDefault(project => project.Name == projectName);
        }


        [CanBeNull]
        public static ICSharpFile GetCSharpFile(this IProject project, string filename)
        {            
            var file = project.GetPsiSourceFileInProject(FileSystemPath.Parse(filename));
            return file?.GetPsiFiles<CSharpLanguage>().SafeOfType<ICSharpFile>().SingleOrDefault();
        }


        [CanBeNull]
        public static ITreeNode GetTypeTreeNodeByFqn(this ICSharpFile file, string typeName)
        {
            var namespaceName = GetLongNameFromFqn(typeName);
            var shortName = GetShortNameFromFqn(typeName);            

            var namespaceDecls = file.NamespaceDeclarationsEnumerable;
            var namespaceDecl = (from decl in namespaceDecls
                where decl.DeclaredName == namespaceName
                select decl).FirstOrDefault();

            if (namespaceDecl == null) return null;
            var typeDecls = namespaceDecl.TypeDeclarationsEnumerable;

            var resultList = (from node in typeDecls
                where node.DeclaredName == shortName
                select node).ToList();

            return resultList.FirstOrDefault();
        }


        public static void NavigateToTypeNodeByFqn(this ISolution solution, string projectName, string fileName, string typeName)
        {                        
            solution.Locks.TryExecuteWithReadLock(() =>
            {
                var project = solution.GetProjectByName(projectName);                
                var file = project.GetCSharpFile(fileName);
                var node = file.GetTypeTreeNodeByFqn(typeName);
                node.NavigateToTreeNode(true);
            });            
        }


        private static string GetShortNameFromFqn(string fqn)
        {
            var pos = fqn.LastIndexOf(".", StringComparison.Ordinal) + 1;
            return pos > 0 ? fqn.Substring(pos) : fqn;
        }


        private static string GetLongNameFromFqn(string fqn)
        {
            var pos = fqn.LastIndexOf(".", StringComparison.Ordinal) + 1;
            return pos > 0 ? fqn.Substring(0, pos - 1) : fqn;
        }


        [CanBeNull]
        public static IEnumerable<IDeclaredElement> GetReferencedElements(this ITreeNode node)
        {
            var result = new List<IDeclaredElement>();
            var parentExpression = node.GetParentOfType<IReferenceExpression>();
            if (parentExpression == null) return null;

            var references = parentExpression.GetReferences();

            foreach (var reference in references)
            {
                var declaredElement = reference.Resolve().DeclaredElement;
                if (declaredElement != null)
                    result.Add(declaredElement);
            }

            return result.Count == 0 ? null : result;
        }


        [CanBeNull]
        public static T GetParentOfType<T>(this ITreeNode node) where T : class, ITreeNode
        {
            while (node != null)
            {
                var typedNode = node as T;
                if (typedNode != null)
                    return typedNode;

                node = node.Parent;
            }
            return null;
        }
    }
}