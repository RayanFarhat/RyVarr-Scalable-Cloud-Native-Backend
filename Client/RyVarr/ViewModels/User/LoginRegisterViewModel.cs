using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RyVarr.ViewModels.User;

public partial class LoginRegisterViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _emailLogin = "";
    [ObservableProperty]
    private string _passwordLogin = "";
    [ObservableProperty]
    private ObservableCollection<string> _errorsLogin = new ObservableCollection<string>();

    [ObservableProperty]
    private string _userNameRegister = "";
    [ObservableProperty]
    private ObservableCollection<string> _errorsUserNameRegister = new ObservableCollection<string>();
    [ObservableProperty]
    private string _emailRegister = "";
    [ObservableProperty]
    private ObservableCollection<string> _errorsEmailRegister = new ObservableCollection<string>();
    [ObservableProperty]
    private string _passwordRegister = "";
    [ObservableProperty]
    private ObservableCollection<string> _errorsPasswordRegister = new ObservableCollection<string>();
    [ObservableProperty]
    private ObservableCollection<string> _errorsRegister = new ObservableCollection<string>();
}
