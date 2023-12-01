namespace BackendServer.DTOs;

using System.ComponentModel.DataAnnotations;


[Serializable]
[GenerateSerializer]
public record ContactData
{
    [Key]
    [Id(0)]
    public string Id { get; set; } = "";

    [Required]
    [Id(1)]
    public string Name { get; set; } = "";

    [Required]
    [Id(2)]
    public string Email { get; set; } = "";

    [Required]
    [Id(3)]
    public string Company { get; set; } = "";

    [Required]
    [Id(4)]
    public string Message { get; set; } = "";

    public ContactData(string Id, string Name, string Email, string Company, string Message)
    {
        this.Id = Id;
        this.Name = Name;
        this.Email = Email;
        this.Company = Company;
        this.Message = Message;
    }
}