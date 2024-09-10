using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("RecipCalendars")]
[PrimaryKey(nameof(UserId), nameof(RecipId), nameof(Date))]
public class RecipCalendarModel
{
    public required string UserId { get; set; }
    [ForeignKey("UserId")]
    public UserModel? User { get; set; }

    public Guid RecipId { get; set; }
    [ForeignKey("RecipId")]
    public RecipModel? Recip { get; set; }

    public DateTime Date { get; set; }
}
