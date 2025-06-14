using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Professions;

public class CreateProfessionRequest
{
    [Required] public string Code { get; set; }
    [Required] public string Designation { get; set; }
    [Required] public string Service { get; set; }
    [Required] public string Entreprise { get; set; }
}