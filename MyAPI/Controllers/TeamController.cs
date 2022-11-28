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
    public class TeamController : ControllerBase
    {
        private readonly MyAPIDBContext _context;

        public TeamController(MyAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Team
        [HttpGet]
        public async Task<ActionResult<ApiResult<Team>>> GetTeam()
        {
            var response = new ApiResult<Team>();
            if (_context.Teams == null)
            {
                response.StatusCode = 404;
                response.StatusDescription = "Not Found";
                return response;
            }
            response.StatusCode = 200;
            response.StatusDescription = "Successful Response";
            response.Response = await _context.Teams.ToListAsync();

            return response;

        }


        // PUT: api/Team/5
        [HttpPut("{TeamNo}")]
        public async Task<IActionResult> PutStat(int TeamNo, Team team)
        {
            if (team != team.TeamNo)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(TeamNo))
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

        // POST: api/Teamno/Stat
        [HttpPost("{TeamNo}")]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            try
            {
                
                _context.Teams.Add(team);
                await _context.SaveChangesAsync();
                Response.StatusCode = 201;

                var description = "Sucessful Response. Team added";
                var response = new ApiResult<Team>(team, description, Response.StatusCode);
                await Response.WriteAsJsonAsync(response);
                return new EmptyResult();

            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                var fail = new ApiResult<Team>(null, "Failed Response. Bad Request. " + e.InnerException.Message, Response.StatusCode);
                await Response.WriteAsJsonAsync(fail);
                return new EmptyResult();
            }
        }

        // DELETE: api/TeamNo/2
        [HttpDelete("{TeamNo}")]
        public async Task<IActionResult> DeleteTeam(int Teamno)
        {
            var team = await _context.Teams.FindAsync(team);
            if (team == null)
            {
                Response.StatusCode = 404;
                var failResult = new ApiResult<Team>(null, "Unsuccessful Response. Invalid ID passed", 404);
                await Response.WriteAsJsonAsync(failResult);
                return new EmptyResult();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            Response.StatusCode = 200;
            var successfulResult = new ApiResult<Team>(team, "Sucessful Response. Team Deleted", 200);
            await Response.WriteAsJsonAsync(successfulResult);
            return new EmptyResult();
        }

        private bool TeamExists(int TeamNo)
        {
            return _context.Teams.Any(e => e.TeamNo == TeamNo);
        }
    }
}
