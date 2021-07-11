using System.Linq;
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Pet pet)
    {
      if (id != pet.PetId)
      {
        return BadRequest();
      }

      _db.Entry(pet).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PetExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(int id)
    {
      var pet = await _db.Pets.FindAsync(id);
      if (pet == null)
      {
        return NotFound();
      }

      _db.Pets.Remove(pet);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    private bool PetExists(int id)
    {
      return _db.Pets.Any(e => e.PetId == id);
    }

  }
}
