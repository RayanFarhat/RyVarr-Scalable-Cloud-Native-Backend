using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RyVarr.Models;
using RyVarr.ViewModels.User;

namespace RyVarr.ViewModels;
public partial class UserViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _userName = "User";
    [ObservableProperty]
    private bool _isLogin = false;

    [ObservableProperty]
    private bool _isSigninWindow = true;

    [ObservableProperty]
    public LoginRegisterViewModel _form = new LoginRegisterViewModel();

    [RelayCommand]
    private void SwitchBetweenRegisters()
    {
        IsSigninWindow ^= true;
    }

    [RelayCommand]
    private async Task onLogin()
    {
        LoginReq req = new LoginReq(Form.EmailLogin, Form.PasswordLogin);
        HttpClientHandler clientHandler = new HttpClientHandler();
        IRes? res = await clientHandler.Req<IRes>("/Account/login", "POST", req);
        
        if (res == null)
        {
            Form.ErrorsLogin.Add("Somthing wrong with the login response!");
            return;
        }
        if(res is LoginRes200)
        {
            LoginRes200 res200 = (LoginRes200)res;
            HttpClientHandler.AuthToken = res200.token;
            IsLogin = true;
        }
        else if (res is LoginRes401)
        {
            LoginRes401 res401 = (LoginRes401)res;
            Form.ErrorsLogin.Add(res401.title);
        }
    }
}
