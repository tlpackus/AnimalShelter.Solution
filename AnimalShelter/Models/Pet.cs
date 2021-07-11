using System.ComponentModel.DataAnnotations;

namespace AnimalShelter.Models
{
  public class Pet
  {
    public int PetId { get; set; }
    public string Name { get; set; }
    [Required]
    public string Species { get; set; }
    [Range(0, 300, ErrorMessage = "Age must be between 0 and 300.")]
    public int Age { get; set; }
    public string Gender { get; set; }
  }
}