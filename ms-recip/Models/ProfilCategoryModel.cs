using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("ProfilCategories")]
[PrimaryKey(nameof(ProfilId), nameof(CategoryId))]
public record ProfilCategoryModel
{
    public Guid ProfilId { get; set; }
    [ForeignKey("ProfilId")]
    public ProfilModel? Profil { get; set; }

    public Guid CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public CategoryModel? Category { get; set; }
}
