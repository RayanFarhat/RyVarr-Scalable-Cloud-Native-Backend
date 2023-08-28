namespace BackendServer.DTOs;

using System.ComponentModel.DataAnnotations;


[Serializable]
[GenerateSerializer]
public record AccountData
{
    [Key]
    [Id(0)]
    public string Id { get; set; } = "";

    [Required]
    [Id(1)]
    public string Username { get; set; } = "";

    [Required]
    [Id(2)]
    public bool IsPro { get; set; } = false;

    public AccountData(string Id, string Username, bool IsPro)
    {
        this.Id = Id;
        this.Username = Username;
        this.IsPro = IsPro;
    }
    // need parameterless constructor for  orleans runtime
    public AccountData()
    {
    }
}