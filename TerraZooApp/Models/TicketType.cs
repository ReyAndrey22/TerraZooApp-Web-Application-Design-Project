using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraZooApp.Models;

[Table("TicketType")]
public class TicketType
{
   [Key]
   [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   [Column("id_ticket_type")]
    public int IdTicketType { get; set; }
    [Column("name_ticket_type")]
    [Required]
    public string? NameTicketType { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }

}

