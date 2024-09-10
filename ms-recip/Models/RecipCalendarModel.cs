using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("RecipCalendars")]
[PrimaryKey(nameof(RecipCalendarModel.UserId), nameof(RecipCalendarModel.RecipId), nameof(RecipCalendarModel.Date))]
public class RecipCalendarModel
{
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public UserModel User { get; set; }

    public Guid RecipId { get; set; }
    [ForeignKey("RecipId")]
    public RecipModel Recip { get; set; }

    public DateTime Date { get; set; }
}
