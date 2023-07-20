using Avalonia.Controls;
using RyVarr.ViewModels;

namespace RyVarr.Views;

public partial class NavBarView : UserControl
{
    public NavBarView()
    {
        InitializeComponent();
        DataContext = MainViewModel.GetInstance();
    }
}
