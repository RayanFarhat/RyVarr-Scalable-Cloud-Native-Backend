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

    [Required]
    [Id(3)]
    //dd-MM-yyyy hh:mm:ss
    public string ProEndingDate { get; set; } = "";

    public AccountData(string Id, string Username, bool IsPro, string ProEndingDate)
    {
        this.Id = Id;
        this.Username = Username;
        this.IsPro = IsPro;
        this.ProEndingDate = ProEndingDate;
    }
    // need parameterless constructor for  orleans runtime
    public AccountData()
    {
    }
}