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

        [HttpPost("actor/{id}")]
        public IActionResult AddActorToScene(int id, List<string> username)
        {
            var result = _sceneRepo.AddActorToScene(id, username);
            return Ok(result);
        }

        // PUT: api/TblScenes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblScene(int id, TblScene tblScene)
        {
            if (id != tblScene.Id)
            {
                return BadRequest();
            }

            var t = _context.TblScene.Where(scene => scene.Id == id).FirstOrDefault();
            if (t != null)
            {
                t.Name = tblScene.Name;
                t.NumberOfShotScenes = tblScene.NumberOfShotScenes;
                t.StartDay = tblScene.StartDay;
                t.EndDay = tblScene.EndDay;
                t.Director = tblScene.Director;
                t.FileDocOfRole = tblScene.FileDocOfRole;
                t.Description = tblScene.Description;
                t.Lastmodified = DateTime.Now;
            }

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

            return Ok();
        }

        // POST: api/TblScenes
        [HttpPost]
        public  IActionResult PostTblScene(TblScene tblScene)
        {
            var result = _sceneRepo.AddNewScene(tblScene);
            if (result == true)
            {
                return Ok(result);
            }
            return BadRequest();
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
