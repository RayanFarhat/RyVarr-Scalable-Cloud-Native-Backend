namespace BackendServer.DTOs;

using System.ComponentModel.DataAnnotations;
public record AcountData
{
    [Key]
    public int Id { get; init; }

    [Required]
    public string Username { get; init; }

    [Required]
    public bool IsPro { get; init; }

    // todo make grain that store this class as state and then do cache write through with the accountData table
    public AcountData(int Id, string Username, bool IsPro)
    {
        this.Id = Id;
        this.Username = Username;
        this.IsPro = IsPro;
    }
}