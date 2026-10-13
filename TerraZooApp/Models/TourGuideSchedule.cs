using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraZooApp.Models;

[Table("TourGuideSchedule")]
public class TourGuideSchedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_tour_guide_schedule")]
    public int IdTourGuideSchedule { get; set; }

    [Column("day_of_week")]
    public string? DayOfWeek { get; set; }

    [Column("start_hour")]
    [DataType(DataType.Time)]
    public TimeOnly StartHour { get; set; }

    [Column("end_hour")]
    [DataType(DataType.Time)]
    public TimeOnly EndHour { get; set; }

    [ForeignKey("Users")]
    [Column("id_users")]
    public string? UserId { get; set; }

    public Users? User { get; set; }
}
