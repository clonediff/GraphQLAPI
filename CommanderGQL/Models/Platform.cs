using System.ComponentModel.DataAnnotations;

namespace CommanderGQL.Models;

public class Platform
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = default!;
    
    [Required]
    public string LicenseKey { get; set; } = default!;
}
