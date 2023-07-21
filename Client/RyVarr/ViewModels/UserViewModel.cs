using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RyVarr.ViewModels;
public partial class UserViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _userName = "User";
    [ObservableProperty]
    private bool _isLogin = true;
    [ObservableProperty]
    private bool _isNotLogin = true;

    [ObservableProperty]
    private bool _isSignin = true;
    [ObservableProperty]
    private bool _isSignUp = false;
    [RelayCommand]
    private void SwitchBetweenRegisters()
    {
        IsSignin ^= true;
        IsSignUp ^= true;
    }
}
