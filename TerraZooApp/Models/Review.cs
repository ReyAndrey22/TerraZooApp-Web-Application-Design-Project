using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraZooApp.Models;

[Table("Review")]
public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_review")]
    public int IdReview { get; set; }

    [Column("rating")]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Column("comment")]
    public string? Comment { get; set; }

    [Column("date_review")]
    [DataType(DataType.Date)]
    public DateOnly DateReview { get; set; }

    [ForeignKey("Users")]
    [Column("id_users")]
    public string? UserId { get; set; }

    public Users? User { get; set; }

    [ForeignKey("Animal")]
    [Column("id_animal")]
    public int? IdAnimal { get; set; }

    public Animal? Animal { get; set; }
}
