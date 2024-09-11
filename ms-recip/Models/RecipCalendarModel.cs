using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("RecipCalendars")]
public class RecipCalendarModel
{
    [Key]
    public Guid Id { get; set; }
    
    public required string UserId { get; set; }
    [ForeignKey("UserId")]
    public UserModel? User { get; set; }

    public Guid RecipId { get; set; }
    [ForeignKey("RecipId")]
    public RecipModel? Recip { get; set; }

    public DateTime Date { get; set; }
}
