using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanAlimentatieSiAntrenament.data;
using PlanAlimentatieSiAntrenament.models;

namespace PlanAlimentatieSiAntrenament.Controllers
{   [ApiController]
    [Route("api/[controller]")]
    public class AlimentatieController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlimentatieController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Alimentatie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alimentatie>>> GetAlimentatie()
        {
            return await _context.Alimentatie.ToListAsync();
        }

        // GET: api/Alimentatie/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Alimentatie>> GetAlimentatie(int id)
        {
            var alimentatie = await _context.Alimentatie.FindAsync(id);

            if (alimentatie == null)
            {
                return NotFound();
            }

            return alimentatie;
        }

        // POST: api/Alimentatie
        [HttpPost]
        public async Task<ActionResult<Alimentatie>> PostAlimentatie(Alimentatie alimentatie)
        {
            _context.Alimentatie.Add(alimentatie);
            await _context.SaveChangesAsync();

            // Dacă este un nou obiect, creăm un 201 Created cu locația acestuia
            return CreatedAtAction(nameof(GetAlimentatie), new { id = alimentatie.Id }, alimentatie);
        }

        // PUT: api/Alimentatie/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlimentatie(int id, Alimentatie alimentatie)
        {
            if (id != alimentatie.Id)
            {
                return BadRequest();
            }

            _context.Entry(alimentatie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentatieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Răspuns 204 No Content
        }

        // DELETE: api/Alimentatie/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlimentatie(int id)
        {
            var alimentatie = await _context.Alimentatie.FindAsync(id);
            if (alimentatie == null)
            {
                return NotFound();
            }

            _context.Alimentatie.Remove(alimentatie);
            await _context.SaveChangesAsync();

            return NoContent(); // Răspuns 204 No Content
        }

        private bool AlimentatieExists(int id)
        {
            return _context.Alimentatie.Any(e => e.Id == id);
        }
    }
}
