using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("Profils")]
public record ProfilModel
{
    [Key]
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public bool Deleted { get; set; } = false;

    public IEnumerable<UserModel>? Users { get; set; }

    public IEnumerable<ProfilCategoryModel>? Categories { get; set; }
}
