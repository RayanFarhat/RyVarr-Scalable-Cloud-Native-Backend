using System.ComponentModel.DataAnnotations;

namespace BackendServer.Authentication;

public class ForgotPasswordModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";
}