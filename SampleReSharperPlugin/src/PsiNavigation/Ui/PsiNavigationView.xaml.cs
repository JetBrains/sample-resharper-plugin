using JetBrains.UI.Wpf;

namespace SampleReSharperPlugin
{
    public partial class PsiNavigationView : IView<PsiNavigationViewModel>
    {
        public new string Name => "PSI Navigation";

        public PsiNavigationView()
        {
            InitializeComponent();
        }
    }
}