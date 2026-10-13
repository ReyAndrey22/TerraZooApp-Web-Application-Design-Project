using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraZooApp.Models;

[Table("Employee")]
public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_employee")]
    public int IdEmployee { get; set; }

    [Column("first_name")]
    public string? FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("email")]
    [EmailAddress]
    public string? Email { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("adress")]
    public string? Address { get; set; }

    [Column("hire_date")]
    [DataType(DataType.Date)]
    public DateOnly HireDate { get; set; }

    [Column("salary")]
    public decimal Salary { get; set; }

    [Column("position")]
    public string? Position { get; set; }
}
