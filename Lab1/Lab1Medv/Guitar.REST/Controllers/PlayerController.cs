using Guitar.Infrastructure;
using Guitar.Infrastructure.Models;
using Guitar.REST.Models; // or wherever Player is defined
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Guitar.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly GuitarContext _context;

        public PlayerController(GuitarContext context)
        {
            _context = context;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerModel>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Player/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerModel>> GetPlayer(Guid id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
                return NotFound();

            return player;
        }

        // POST: api/Player
        [HttpPost]
        public async Task<ActionResult<PlayerModel>> PostPlayer(PlayerModel player)
        {
            player.Id = Guid.NewGuid();
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }

        // PUT: api/Player/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(Guid id, PlayerModel player)
        {
            if (id != player.Id)
                return BadRequest();

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Players.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Player/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
                return NotFound();

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}