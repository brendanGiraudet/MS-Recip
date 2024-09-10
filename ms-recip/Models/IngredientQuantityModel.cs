using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("IngredientQuantities")]
[PrimaryKey(nameof(IngredientQuantityModel.RecipId), nameof(IngredientQuantityModel.IngredientId))]
public record IngredientQuantityModel
{
    public decimal Quantity { get; set; }

    public string MeasureUnit { get; set; }

    public Guid RecipId { get; set; }
    [ForeignKey("RecipId")]
    public RecipModel Recip { get; set; }

    public Guid IngredientId { get; set; }
    [ForeignKey("IngredientId")]
    public IngredientModel Ingredient { get; set; }
}
