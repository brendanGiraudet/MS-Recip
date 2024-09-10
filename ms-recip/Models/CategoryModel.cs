using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("Categories")]
public record CategoryModel
{
    [Key]
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public bool Deleted { get; set; } = false;
    
    public IEnumerable<RecipCategoryModel>? Recips { get; set; }
    
    public IEnumerable<ProfilCategoryModel>? Profils { get; set; }
}
