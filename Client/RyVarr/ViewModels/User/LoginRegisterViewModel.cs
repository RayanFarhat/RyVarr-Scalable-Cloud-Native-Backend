using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RyVarr.ViewModels.User;

public partial class LoginRegisterViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _emailLogin = "22";
    [ObservableProperty]
    private string _passwordLogin = "22";
    [ObservableProperty]
    private ObservableCollection<string> _errorsLogin = new ObservableCollection<string>();

    [ObservableProperty]
    private string _userNameRegister = "";
    [ObservableProperty]
    private string _emailRegister = "";
    [ObservableProperty]
    private string _passwordRegister = "";
    [ObservableProperty]
    private ObservableCollection<string> _errorsRegister = new ObservableCollection<string>();
    //todo make 3 error list

}
