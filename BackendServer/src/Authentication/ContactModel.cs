using System.ComponentModel.DataAnnotations;

namespace BackendServer.Authentication;

public class ContactModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = "";
    public string Company { get; set; } = "";

    [Required(ErrorMessage = "Message is required")]
    public string Message { get; set; } = "";
}
