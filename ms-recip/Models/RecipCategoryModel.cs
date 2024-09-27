using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("RecipCategories")]
[PrimaryKey(nameof(RecipId), nameof(CategoryId))]
public record RecipCategoryModel
{
    [Key]
    public Guid RecipId { get; set; }
    [ForeignKey("RecipId")]
    public RecipModel? Recip { get; set; }

    [Key]
    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public CategoryModel? Category { get; set; }
}
