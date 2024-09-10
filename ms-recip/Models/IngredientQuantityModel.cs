using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("IngredientQuantities")]
[PrimaryKey(nameof(RecipId), nameof(IngredientId))]
public record IngredientQuantityModel
{
    public required decimal Quantity { get; set; }

    public required string MeasureUnit { get; set; }

    public Guid RecipId { get; set; }
    [ForeignKey("RecipId")]
    public RecipModel? Recip { get; set; }

    public Guid IngredientId { get; set; }
    [ForeignKey("IngredientId")]
    public IngredientModel? Ingredient { get; set; }
}
