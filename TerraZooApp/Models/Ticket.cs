using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraZooApp.Models;

[Table("Ticket")]
public class Ticket
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_ticket")]
    public int IdTicket { get; set; }

    [Column("purchase_date")]
    [DataType(DataType.Date)]
    public DateOnly PurchaseDate { get; set; }

    [ForeignKey("Users")]
    [Column("id_users")]
    public string? UserId { get; set; }

    public Users? User { get; set; }

    [ForeignKey("Tour")]
    [Column("id_tour")]
    public int? IdTour { get; set; }

    public Tour? Tour { get; set; }
    [ForeignKey("TicketType")]
    [Column("id_ticket_type")]
    public int IdTicketType { get; set; }
    public TicketType? TicketType { get; set; }
}
