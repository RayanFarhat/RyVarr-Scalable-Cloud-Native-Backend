using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using RyVarr.ViewModels;

namespace RyVarr.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContext = MainViewModel.GetInstance();
        // when view attached to the root
        this.AttachedToVisualTree += OnAttachedToVisualTree;
        
    }
    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        //allow using Clipboard
        if(DataContext  != null) {
            ((MainViewModel)DataContext).Clipboard = TopLevel.GetTopLevel(this)?.Clipboard;
            ((MainViewModel)DataContext).StorageProvider = TopLevel.GetTopLevel(this)?.StorageProvider;
        }
    }

}
