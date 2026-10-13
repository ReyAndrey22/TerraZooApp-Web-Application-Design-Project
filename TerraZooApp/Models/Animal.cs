using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraZooApp.Models;

[Table("Animal")]
public class Animal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_animal")]
    public int IdAnimal { get; set; }

    [Column("name")]
    public string? NameAnimal { get; set; }

    [Column("species")]
    public string? Species { get; set; }

    [Column("scientific_name")]
    public string? ScientificName { get; set; }

    [Column("breed")]
    public string? Breed { get; set; }

    [Column("sex")]
    public string? Sex { get; set; }

    [Column("age")]
    public int? Age { get; set; }

    [Column("natural_habitat")]
    public string? NaturalHabitat { get; set; }

    [Column("diet_type")]
    public string? DietType { get; set; }

    [Column("image_animal")]
    public string? ImageAnimal { get; set; }
    [NotMapped]
    public IFormFile? ImageAnimalFile { get; set; }

    public ICollection<Review>? Reviews { get; set; }
}
