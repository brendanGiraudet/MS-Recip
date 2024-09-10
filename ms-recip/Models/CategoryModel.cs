using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("Categories")]
public record CategoryModel
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public bool Deleted { get; set; }
    
    public IEnumerable<RecipCategoryModel> Recips { get; set; }
}
