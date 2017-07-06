using System;
using System.Windows.Controls;
using JetBrains.ActionManagement;
using JetBrains.Application;
using JetBrains.Application.Settings;
using JetBrains.DataFlow;
using JetBrains.DocumentManagers;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl;
using JetBrains.Threading;
using JetBrains.UI.Application;
using JetBrains.UI.Components.Theming;
using JetBrains.UI.Controls;
using JetBrains.UI.CrossFramework;
using JetBrains.UI.ToolWindowManagement;
using JetBrains.Util;

namespace SampleReSharperPlugin
{
    public class SampleToolWindow
    {
        private readonly TabbedToolWindowClass _toolWindowClass;
        private readonly ToolWindowInstance _toolWindowInstance;

        public SampleToolWindow(Lifetime lifetime, ToolWindowManager toolWindowManager,
            SampleToolWindowDescriptor sampleToolWindowDescriptor, ISettingsStore settingsStore,
            IColorThemeManager colorThemeManager)
        {
            _toolWindowClass = toolWindowManager.Classes[sampleToolWindowDescriptor] as TabbedToolWindowClass;
            if (_toolWindowClass == null)
                throw new ApplicationException("ToolWindowClass");

            _toolWindowInstance = _toolWindowClass.RegisterInstance(lifetime, "Sample Tool Window", null,
                (lt, twi) =>
                {
                    twi.QueryClose.Value = true;

                    var toolPanel = new ToolPanel();

                    // Tool Window
                    var toolwndView = new ToolWindowView();
                    var toolwndViewTab = new TabItem
                    {
                        Content = toolwndView,
                        Header = toolwndView.Name
                    };
                    toolPanel.tabControl.Items.Add(toolwndViewTab);

                    // Options 
                    var optionsPageViewModel = new OptionsPageViewModel(lt, settingsStore);
                    var optionsPageView = new OptionsPageView {DataContext = optionsPageViewModel};
                    var optionsPageViewTab = new TabItem
                    {
                        Content = optionsPageView,
                        Header = optionsPageView.Name
                    };
                    toolPanel.tabControl.Items.Add(optionsPageViewTab);

                    // Actions
                    var actionsViewModel = new ActionsViewModel(lt);
                    var actionsView = new ActionsView {DataContext = actionsViewModel};
                    var actionsViewTab = new TabItem
                    {
                        Content = actionsView,
                        Header = actionsView.Name
                    };
                    toolPanel.tabControl.Items.Add(actionsViewTab);

                    // PSI Navigation                                        
                    var psiNavigationViewModel = new PsiNavigationViewModel(lt);
                    var psiNavigationView = new PsiNavigationView {DataContext = psiNavigationViewModel};
                    var psiNavigationViewTab = new TabItem
                    {
                        Content = psiNavigationView,
                        Header = psiNavigationView.Name
                    };
                    toolPanel.tabControl.Items.Add(psiNavigationViewTab);

                    // Context Action                    
                    var contextActionView = new ToLowerCaseContextActionView();
                    var contextActionViewTab = new TabItem
                    {
                        Content = contextActionView,
                        Header = contextActionView.Name
                    };
                    toolPanel.tabControl.Items.Add(contextActionViewTab);

                    // Quick Fix
                    var quickFixView = new CorrectBadWordQuickFixView();
                    var quickFixViewTab = new TabItem
                    {
                        Content = quickFixView,
                        Header = quickFixView.Name
                    };
                    toolPanel.tabControl.Items.Add(quickFixViewTab);

                    // Element Problem Analyzer and Code Highlighting
                    var problemAnalyzerView = new BadWordNamingView();
                    var problemAnalyzerViewTab = new TabItem
                    {
                        Content = problemAnalyzerView,
                        Header = problemAnalyzerView.Name
                    };
                    toolPanel.tabControl.Items.Add(problemAnalyzerViewTab);

                    // Solution Component
                    var solcompView = new SolutionComponentView();
                    var solcompViewTab = new TabItem
                    {
                        Content = solcompView,
                        Header = solcompView.Name
                    };
                    toolPanel.tabControl.Items.Add(solcompViewTab);

                    // Signals
                    var sigViewModel = new SignalsViewModel(lt);
                    var sigView = new SignalsView {DataContext = sigViewModel};
                    var sigViewTab = new TabItem
                    {
                        Content = sigView,
                        Header = sigView.Name
                    };
                    toolPanel.tabControl.Items.Add(sigViewTab);

                    // IProperty
                    var propViewModel = new PropertyViewModel(lt);
                    var propView = new PropertyView {DataContext = propViewModel};
                    var propViewTab = new TabItem
                    {
                        Content = propView,
                        Header = propView.Name
                    };
                    toolPanel.tabControl.Items.Add(propViewTab);

                    // UI Themes
                    var uithemeViewModel = new UiThemeViewModel(lt, colorThemeManager);
                    var uithemeView = new UiThemeView {DataContext = uithemeViewModel};
                    var uithemeViewTab = new TabItem
                    {
                        Content = uithemeView,
                        Header = uithemeView.Name
                    };
                    toolPanel.tabControl.Items.Add(uithemeViewTab);

                    return new EitherControl(lt, toolPanel);
                });
        }

        public void Show()
        {
            _toolWindowClass.Show();
        }
    }
}