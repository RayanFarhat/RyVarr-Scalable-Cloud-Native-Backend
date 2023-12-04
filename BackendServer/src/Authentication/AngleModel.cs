using System.ComponentModel.DataAnnotations;

namespace BackendServer.Authentication;

public class AngleModel
{
    [Required(ErrorMessage = "p1x is required")]
    public double p1x { get; set; }

    [Required(ErrorMessage = "p1x is required")]
    public double p1y { get; set; }
    [Required(ErrorMessage = "p1x is required")]
    public double p2x { get; set; }
    [Required(ErrorMessage = "p1x is required")]
    public double p2y { get; set; }
}
