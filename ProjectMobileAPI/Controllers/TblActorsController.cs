using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMobileAPI.Models;
using ProjectMobileAPI.Repositories;

namespace ProjectMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblActorsController : ControllerBase
    {
        private readonly ProjectMobileContext _context;
        private readonly ActorRepository _repo;

        public TblActorsController(ProjectMobileContext context)
        {
            _context = context;
            _repo = new ActorRepository(_context);
        }

        // GET: api/TblActors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblActor>>> GetTblActor()
        {
            return await _context.TblActor
                .Include(actor => actor.TblSceneActor)
                .ToListAsync();
        }

        // GET: api/TblActors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblActor>> GetTblActor(string id)
        {
            var tblActor = await _context.TblActor
                .Include(i => i.TblSceneActor)
                .FirstOrDefaultAsync(i => i.Username == id);

            if (tblActor == null)
            {
                return NotFound();
            }

            return tblActor;
        }

        [HttpGet("available/{id}")]
        public IActionResult GetAvailableActor(int id)
        {
            var result = _repo.GetAvailableActor(id);
            return Ok(result);
        }

        // PUT: api/TblActors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblActor(string id, TblActor tblActor)
        {
            if (id != tblActor.Username)
            {
                return BadRequest();
            }

            _context.Entry(tblActor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblActorExists(id))
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

        // POST: api/TblActors
        [HttpPost]
        public async Task<ActionResult<TblActor>> PostTblActor(TblActor tblActor)
        {
            _context.TblActor.Add(tblActor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TblActorExists(tblActor.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTblActor", new { id = tblActor.Username }, tblActor);
        }

        // DELETE: api/TblActors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblActor>> DeleteTblActor(string id)
        {
            var tblActor = await _context.TblActor.FindAsync(id);
            if (tblActor == null)
            {
                return NotFound();
            }

            _context.TblActor.Remove(tblActor);
            await _context.SaveChangesAsync();

            return tblActor;
        }

        private bool TblActorExists(string id)
        {
            return _context.TblActor.Any(e => e.Username == id);
        }
    }
}
