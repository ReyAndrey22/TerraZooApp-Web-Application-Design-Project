using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TerraZooApp.Models;

[Table("Users")]
public class Users : IdentityUser
{
    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }
    [Column("role")]
    public string? Role { get; set; }
    [Column("profile_picture")]
    public string? ProfilePicture { get; set; }
    
    [NotMapped]
    public IFormFile? ProfilePictureFile { get; set; }

    public ICollection<Review>? Reviews { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }
    public ICollection<Tour>? Tours { get; set; }
    public ICollection<TourGuideSchedule>? Schedules { get; set; }
}
