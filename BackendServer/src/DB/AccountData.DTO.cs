namespace BackendServer.DTOs;

using System.ComponentModel.DataAnnotations;


[Serializable]
public record AccountData
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string Username { get; init; }

    [Required]
    public bool IsPro { get; init; }

    public AccountData(int Id, string Username, bool IsPro)
    {
        this.Id = Id;
        this.Username = Username;
        this.IsPro = IsPro;
    }
}