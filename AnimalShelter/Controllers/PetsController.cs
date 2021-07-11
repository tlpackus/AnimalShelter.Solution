using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelter.Models;

namespace AnimalShelter.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetsController : ControllerBase
  {
    private readonly AnimalShelterContext _db;

    public PetsController(AnimalShelterContext db)
    {
      _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> Get()
    {
      return await _db.Pets.ToListAsync();
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Pet>> GetPet(int id)
    {
        var pet = await _db.Pets.FindAsync(id);

        if (pet == null)
        {
            return NotFound();
        }

        return pet;
    }

    [HttpPost]
    public async Task<ActionResult<Pet>> Post(Pet pet)
    {
      _db.Pets.Add(pet);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetPet), new { id = pet.PetId }, pet);
    }
  }
}
