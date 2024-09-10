using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("Users")]
public record UserModel
{
    [Key]
    public required string UserId { get; set; }
    
    public required Guid ProfilId { get; set; }
    [ForeignKey("ProfilId")]
    public ProfilModel? Profil { get; set; }

    public bool Deleted { get; set; } = false;
    
    public IEnumerable<RecipCalendarModel>? RecipCalendars { get; set; }
}
