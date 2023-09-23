using Avalonia.Controls;
using RyVarr.ViewModels;
namespace RyVarr.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = MainViewModel.GetInstance();
    }
}
