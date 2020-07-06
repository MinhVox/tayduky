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
    public class TblToolsController : ControllerBase
    {
        private readonly ProjectMobileContext _context;
        private readonly ToolRepository _toolRepo;

        public TblToolsController(ProjectMobileContext context)
        {
            _context = context;
            _toolRepo = new ToolRepository(_context);
        }

        // GET: api/TblTools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblTool>>> GetTblTool()
        {
            return await _context.TblTool
                .Include(i => i.TblSceneTool)
                .ToListAsync();
        }

        // GET: api/TblTools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblTool>> GetTblTool(int id)
        {
            var tblTool = await _context.TblTool.FindAsync(id);

            if (tblTool == null)
            {
                return NotFound();
            }

            return tblTool;
        }

        [HttpGet("available/{id}")]
        public IActionResult GetAvailableTool(int id)
        {
            var result = _toolRepo.GetAvailableTool(id);
            return Ok(result);
        }

        // PUT: api/TblTools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblTool(int id,TblTool tblTool)
        {
            if (id != tblTool.Id)
            {
                return BadRequest();
            }

            var t = _context.TblTool.Where(tool => tool.Id == id).FirstOrDefault();
            if(t != null)
            {
                t.Name = tblTool.Name;
                t.Amount = tblTool.Amount;
                t.Description = tblTool.Description;
                t.Img = tblTool.Img;
                t.Status = true;
                t.Username = tblTool.Username;
                t.LastModified = DateTime.Now;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblToolExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(true);
        }

        // POST: api/TblTools
        [HttpPost]
        public IActionResult AddNewTool(TblTool tblTool)
        {
            var result = _toolRepo.AddNewTool(tblTool);
            if(result == true)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // DELETE: api/TblTools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblTool>> DeleteTblTool(int id)
        {
            var tblTool = await _context.TblTool.FindAsync(id);
            if (tblTool == null)
            {
                return NotFound();
            }

            _context.TblTool.Remove(tblTool);
            await _context.SaveChangesAsync();

            return tblTool;
        }

        private bool TblToolExists(int id)
        {
            return _context.TblTool.Any(e => e.Id == id);
        }
    }
}
