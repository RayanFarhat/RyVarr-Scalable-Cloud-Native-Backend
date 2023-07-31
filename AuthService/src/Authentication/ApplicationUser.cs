using Microsoft.AspNetCore.Identity;

namespace AuthService.Authentication;
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = "";
    public string DateOfBirth { get; set; } = "";
    public string Password { get; set; } = "";
}