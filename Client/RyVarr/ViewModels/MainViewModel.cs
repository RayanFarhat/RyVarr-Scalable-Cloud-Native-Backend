using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RyVarr.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _mainPageIsOpen = true;

    [RelayCommand]
    private void OpenMainPage()
    {
        MainPageIsOpen = true;
        UserPageIsOpen = false;
        ProjectPageIsOpen = false;
    }

    [ObservableProperty]
    private bool _userPageIsOpen = false;

    [RelayCommand]
    private void OpenUserPage()
    {
        MainPageIsOpen = false;
        UserPageIsOpen = true;
        ProjectPageIsOpen = false;
    }

    [ObservableProperty]
    private bool _projectPageIsOpen = false;

    [RelayCommand]
    private void OpenProjectPage()
    {
        MainPageIsOpen = false;
        UserPageIsOpen = false;
        ProjectPageIsOpen = true;
    }

    [ObservableProperty]
    public UserViewModel _userModel = new UserViewModel();
    [ObservableProperty]
    public ProjectsViewModel _projectsModel = new ProjectsViewModel();


    // Ctrl-C to msg string
    public IClipboard? Clipboard;
    [RelayCommand]
    private async Task CopyButton_OnClick(string msg)
    {
        if (Clipboard != null) { await Clipboard.SetTextAsync(msg); }
    }

    ///////////////////////// Singleton //////////////////////////////////
    // Private static instance of the ViewModelBase class
    private static MainViewModel? _instance = null;

    // Private constructor to prevent external instantiation
    private MainViewModel()// made public for design
    {
    }

    // Public static method to access the singleton instance
    public static MainViewModel GetInstance()
    {
        // Create the instance if it doesn't exist
        if (_instance == null)
        {
            _instance = new MainViewModel();
        }

        return _instance;
    }
    ///////////////////////////////////////////////////////////
}
