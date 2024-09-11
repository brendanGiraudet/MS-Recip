using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("Recips")]
public record RecipModel
{
    [Key]
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Image { get; set; }
    
    public required string Authorname { get; set; }
    
    public required int PersonNumber { get; set; }
    
    public required DateTime? CookingDuration { get; set; }
    
    public bool Deleted { get; set; } = false;

    public IEnumerable<RecipStepModel>? Steps { get; set; }
    
    public IEnumerable<RecipCategoryModel>? Categories { get; set; }
    
    public IEnumerable<IngredientQuantityModel>? IngredientQuantities { get; set; }
}
