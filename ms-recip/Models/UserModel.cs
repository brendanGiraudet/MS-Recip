using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("Users")]
public record UserModel
{
    [Key]
    public string UserId { get; set; }
    
    public Guid ProfilId { get; set; }
    [ForeignKey("ProfilId")]
    public ProfilModel Profil { get; set; }
    
    public bool Deleted { get; set; }
    
    public IEnumerable<RecipCalendarModel> RecipCalendars { get; set; }
}
