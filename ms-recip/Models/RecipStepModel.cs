using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_recip.Models;

[Table("RecipSteps")]
public record RecipStepModel
{
    [Key]
    public Guid Id { get; set; }
    
    public string Label { get; set; }
    
    public Guid RecipId { get; set; }
    [ForeignKey("RecipId")]
    public RecipModel Recip { get; set; }
}
