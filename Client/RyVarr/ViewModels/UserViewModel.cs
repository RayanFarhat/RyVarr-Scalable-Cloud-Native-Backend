using System;
using System.Collections.ObjectModel;
using System.Linq;
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
    private bool _onRegisterSuccess = false;

    [ObservableProperty]
    public LoginRegisterViewModel _form = new LoginRegisterViewModel();

    [RelayCommand]
    private void SwitchBetweenRegisters()
    {
        IsSigninWindow ^= true;
    }


    [RelayCommand]
    private async Task onRegister()
    {
        RegisterReq req = new RegisterReq(Form.UserNameRegister, Form.EmailRegister, Form.PasswordRegister);
        HttpClientHandler clientHandler = new HttpClientHandler();
        var res = await clientHandler.Req<RegisterReq>("/api/Account/register", "POST", req);
        if (res == null)
        {
            Form.ErrorsRegister.Clear();
            Form.ErrorsRegister.Add("Somthing wrong with the register response!");
            return;
        }
        if (res.IsSuccessStatusCode)
        {
            RegisterRes200? res200 = await clientHandler.Deserialize<RegisterRes200>(res);
            Form.ErrorsUserNameRegister.Clear();
            Form.ErrorsEmailRegister.Clear();
            Form.ErrorsPasswordRegister.Clear();
            Form.ErrorsRegister.Clear();
            if (res200 == null)
            {
                Form.ErrorsRegister.Add("Somthing wrong with Deserialize RegisterRes200");
                return;
            }
            // show thing that tell that the registerations is done
            OnRegisterSuccess = true;
            await Task.Delay(10000);
            OnRegisterSuccess = false;
        }
        else if (res.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            RegisterRes400? res400 = await clientHandler.Deserialize<RegisterRes400>(res);
            if (res400 == null)
            {
                Form.ErrorsUserNameRegister.Clear();
                Form.ErrorsEmailRegister.Clear();
                Form.ErrorsPasswordRegister.Clear();
                Form.ErrorsRegister.Clear();
                Form.ErrorsRegister.Add("Somthing wrong with Deserialize RegisterRes400");
                return;
            }
            if (res400.errors != null) {
                Form.ErrorsRegister.Clear();

                if (res400.errors.Username != null)
                    Form.ErrorsUserNameRegister = new ObservableCollection<string>(res400.errors.Username.ToList());
                if (res400.errors.Email != null)
                    Form.ErrorsEmailRegister = new ObservableCollection<string>(res400.errors.Email.ToList());
                if (res400.errors.Password != null)
                    Form.ErrorsPasswordRegister = new ObservableCollection<string>(res400.errors.Password.ToList());
            }
            else {
                Form.ErrorsUserNameRegister.Clear();
                Form.ErrorsEmailRegister.Clear();
                Form.ErrorsPasswordRegister.Clear();
                Form.ErrorsRegister.Clear();
                Form.ErrorsRegister.Add("res400.errors == null");
                return;
            }
        }
        else
        {
            Form.ErrorsUserNameRegister.Clear();
            Form.ErrorsEmailRegister.Clear();
            Form.ErrorsPasswordRegister.Clear();
            Form.ErrorsRegister.Clear();
            Form.ErrorsRegister.Add($"Got the status {res.StatusCode}");
            return;
        }
    }


    [RelayCommand]
    private async Task onLogin()
    {
        LoginReq req = new LoginReq(Form.EmailLogin, Form.PasswordLogin);
        HttpClientHandler clientHandler = new HttpClientHandler();
        var res = await clientHandler.Req<LoginReq>("/api/Account/login", "POST", req);
        
        if (res == null)
        {
            Form.ErrorsLogin.Clear();
            Form.ErrorsLogin.Add("Somthing wrong with the login response!");
            return;
        }
        if(res.IsSuccessStatusCode)
        {
            LoginRes200? res200 = await clientHandler.Deserialize<LoginRes200>(res);
            if (res200 == null)
            {
                Form.ErrorsLogin.Clear();
                Form.ErrorsLogin.Add("Somthing wrong with Deserialize LoginRes200");
                return;
            }
            HttpClientHandler.AuthToken = res200.token;
            await _getUserDataReq();
        }

        else if (res.StatusCode == System.Net.HttpStatusCode.BadRequest)
        {
            LoginRes400? res400 = await clientHandler.Deserialize<LoginRes400>(res);
            if (res400 == null)
            {
                Form.ErrorsLogin.Clear();
                Form.ErrorsLogin.Add("Somthing wrong with Deserialize LoginRes400");
                return;
            }
            Form.ErrorsLogin = new ObservableCollection<string>(res400.errors.Email.ToList());
        }
        else
        {
            LoginRes401? res401 = await clientHandler.Deserialize<LoginRes401>(res);
            if (res401 == null)
            {
                Form.ErrorsLogin.Clear();
                Form.ErrorsLogin.Add("Somthing wrong with Deserialize LoginRes401");
                return;
            }
            Form.ErrorsLogin.Clear();
            Form.ErrorsLogin.Add("Unauthorized : Your email or password might be wrong!");
        }
    }

    private async Task _getUserDataReq()
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        Form.ErrorsLogin.Add($"Got the token {HttpClientHandler.AuthToken}");
        var res = await clientHandler.Req<IReq>("/api/Account", "GET", null);
        if (res == null)
        {
            //Form.ErrorsLogin.Clear();
            Form.ErrorsLogin.Add("Somthing wrong with getting acount data!");
            return;
        }
        if (res.IsSuccessStatusCode)
        {
            // get user data
            var accountRes200 = await clientHandler.Deserialize<AccountRes200>(res);
            if (accountRes200 == null)
            {
                // Form.ErrorsLogin.Clear();
                Form.ErrorsLogin.Add("Somthing wrong with Deserialize AccountRes200");
                return;
            }
            IsLogin = true;
            UserName = accountRes200.username;
        }
        else
        {
            Form.ErrorsLogin.Add($"Got the status {res.StatusCode}");
        }

    }
}
