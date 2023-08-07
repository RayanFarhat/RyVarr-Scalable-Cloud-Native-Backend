using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RyVarr.ViewModels.User;

public partial class LoginRegisterViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _emailLogin = "";
    [ObservableProperty]
    private string _passwordLogin = "";
    [ObservableProperty]
    private List<string> _errorsLogin = new List<string>();

    [ObservableProperty]
    private string _userNameRegister = "";
    [ObservableProperty]
    private string _emailRegister = "";
    [ObservableProperty]
    private string _passwordRegister = "";
    [ObservableProperty]
    private List<string> _errorsRegister = new List<string>();

}
