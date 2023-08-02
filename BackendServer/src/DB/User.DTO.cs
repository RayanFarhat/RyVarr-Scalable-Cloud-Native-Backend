namespace BackendServer.DTOs;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

public record User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; init; }

    [Required]
    [MaxLength(100)]
    public string username { get; init; }

    [Required]
    [MaxLength(100)]
    public string email { get; init; }

    [Required]
    [MaxLength(100)]
    public string password { get; init; }

    public User(int id, string username, string email, string password)
    {
        this.id = id;
        this.username = username;
        this.email = email;
        this.password = password;
    }
}
public record UserForRegister(string username, string email, string password);
public record UserForlogin(string email, string password);

