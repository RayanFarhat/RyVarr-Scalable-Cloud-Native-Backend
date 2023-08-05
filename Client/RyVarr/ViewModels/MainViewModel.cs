using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
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
        ProjectPageIsOpen = false;
    }

    [ObservableProperty]
    private bool _projectPageIsOpen = false;

    [RelayCommand]
    private void OpenProjectPage()
    {
        MainPageIsOpen = false;
        ProjectPageIsOpen = true;
    }

    [ObservableProperty]
    private bool _userPageIsOpen = false;
    [RelayCommand]
    private void UserBtnPressed()
    {
        UserPageIsOpen ^= true;
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

    // for open and saving files
    public IStorageProvider? StorageProvider;
    [RelayCommand]
    private async Task OpenFile()
    {
        if (StorageProvider != null)
        {
            // Start async operation to open the dialog.
            var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false
            });
            if (files.Count >= 1)
            {
                // Open reading stream from the first file.
                await using var stream = await files[0].OpenReadAsync();
                using var streamReader = new StreamReader(stream);
                // Reads all the content of file as a text.
                var fileContent = await streamReader.ReadToEndAsync();
            }
        }
    }

    ///////////////////////// Singleton //////////////////////////////////
    // Private static instance of the ViewModelBase class
    private static MainViewModel? _instance = null;

    // Private constructor to prevent external instantiation
    public MainViewModel()// made public for design
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
