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
    public class TblScenesController : ControllerBase
    {
        private readonly ProjectMobileContext _context;
        private readonly SceneRepository _sceneRepo;

        public TblScenesController(ProjectMobileContext context)
        {
            _context = context;
            _sceneRepo = new SceneRepository(_context);
        }


        // GET: api/TblScenes
        [HttpGet("{username}")]
        public IActionResult GetTblScene(String username)
        {
            var result = _context.TblScene
                .Where(i => i.Director == username)
                .ToList();

            return Ok(result);
        }

        // GET: api/TblScenes/5
        [HttpGet("{id}/detail")]
        public async Task<ActionResult<TblScene>> GetTblScene(int id)
        {
            var tblScene = await _context.TblScene
                .Include(tool => tool.TblSceneTool)
                .Include(actor => actor.TblSceneActor)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (tblScene == null)
            {
                return NotFound();
            }

            return tblScene;
        }

        // PUT: api/TblScenes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblScene(int id, TblScene tblScene)
        {
            if (id != tblScene.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblScene).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblSceneExists(id))
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

        // POST: api/TblScenes
        [HttpPost]
        public async Task<ActionResult<TblScene>> PostTblScene(TblScene tblScene)
        {
            _context.TblScene.Add(tblScene);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblScene", new { id = tblScene.Id }, tblScene);
        }

        // DELETE: api/TblScenes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblScene>> DeleteTblScene(int id)
        {
            var tblScene = await _context.TblScene.FindAsync(id);
            if (tblScene == null)
            {
                return NotFound();
            }

            _context.TblScene.Remove(tblScene);
            await _context.SaveChangesAsync();

            return tblScene;
        }

        private bool TblSceneExists(int id)
        {
            return _context.TblScene.Any(e => e.Id == id);
        }
    }
}
