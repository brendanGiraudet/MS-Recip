using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("Profils")]
public record ProfilModel
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public IEnumerable<UserModel>? Users { get; set; }

    public required IEnumerable<RecipCategoryModel> Categories { get; set; }
}
