#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Models;
using Newtonsoft.Json;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly MyAPIDBContext _context;

        public PlayerController(MyAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/PlayerStats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/playername/benzema
        [HttpGet("{playername}")]
        public async Task<ActionResult<Player>> GetPlayer(string playername)
        {
            var player = await _context.Players.FindAsync(playername);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/playername/benzema/goals/5
        [HttpPut("{playername}")]
        public async Task<IActionResult> PutStat(string playername, Player player)
        {
            if (playername != player.PlayerName)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(playername))
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

        // POST: api/Player
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.PlayerName }, player);
        }

        // DELETE: api/player/benzema
        [HttpDelete("{playername}")]
        public async Task<IActionResult> DeletePlayer(string playername)
        {
            var player = await _context.Players.FindAsync(playername);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(string playername)
        {
            return _context.Players.Any(e => e.playername == playername);
        }
    }
}
