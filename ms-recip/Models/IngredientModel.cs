using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("Ingredients")]
public record IngredientModel
{
    [Key]
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Image { get; set; }
    
    public bool Deleted { get; set; } = false;
}
