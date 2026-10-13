using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraZooApp.Models;

[Table("Tour")]
public class Tour
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tour")]
    public int IdTour { get; set; }

    [Column("title")]
    [Required]
    public string? Title { get; set; }

    [Column("date_tour")]
    [DataType(DataType.Date)]
    public DateOnly DateTour { get; set; }

    [Column("start_hour")]
    [DataType(DataType.Time)]
    public TimeOnly StartHour { get; set; }

    [Column("end_hour")]
    [DataType(DataType.Time)]
    public TimeOnly EndHour { get; set; }

    [Column("number_spots")]
    public int NumberOfSpots { get; set; }

    [Column("duration")]
    public int Duration { get; set; } 

    [Column("description")]
    public string? Description { get; set; }

    [ForeignKey("Users")]
    [Column("id_users")]
    public string? UserId { get; set; }

    public Users? User { get; set; }

    
    public ICollection<Ticket>? Tickets { get; set; }
}
