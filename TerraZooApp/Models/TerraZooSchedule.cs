using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraZooApp.Models;

[Table("TerraZooSchedule")]
public class TerraZooSchedule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_terrazoo_schedule")]
    public int IdTerraZooSchedule { get; set; }

    [Column("day_of_week")]
    [Required]
    public string? DayOfWeek { get; set; }

    [Column("opening_hour")]
    [DataType(DataType.Time)]
    public TimeOnly OpeningHour { get; set; }

    [Column("closing_hour")]
    [DataType(DataType.Time)]
    public TimeOnly ClosingHour { get; set; }
}
