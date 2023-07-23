using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ryvarr.models;

namespace RyVarr.Models;

public partial class SignUpForm : ObservableValidator
{
    [Required]
    [MaxLength(30)]
    [ObservableProperty]
    private string _userName;

    [Required]
    [EmailAddress]
    [MaxLength(30)]
    [ObservableProperty]
    private string _email;

    [Required]
    [MaxLength(30)]
    [PasswordPropertyText]
    [ObservableProperty]
    private string _password;
    public DataValidator dataValidator { get; set; }

    public SignUpForm(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;

        dataValidator = new DataValidator(this);
        ErrorsChanged += dataValidator.OnError;
    }

    [RelayCommand]
    private void SignUp()
    {
        ValidateAllProperties();
        dataValidator.Validate();
    }
}
