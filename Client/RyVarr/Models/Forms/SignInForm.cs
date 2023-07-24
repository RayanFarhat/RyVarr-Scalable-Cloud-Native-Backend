using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ryvarr.models;

namespace RyVarr.Models.Forms;

public partial class SignInForm : ObservableValidator
{
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

    public SignInForm( string email, string password)
    {
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
