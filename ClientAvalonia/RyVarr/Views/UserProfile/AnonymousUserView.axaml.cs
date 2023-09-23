using Avalonia.Controls;
using RyVarr.ViewModels;

namespace RyVarr.Views.UserProfile
{
    public partial class AnonymousUserView : UserControl
    {
        public AnonymousUserView()
        {
            InitializeComponent();
            DataContext = MainViewModel.GetInstance();
        }
    }
}
