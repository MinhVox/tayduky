﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMobileAPI.Models;

namespace ProjectMobileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblToolsController : ControllerBase
    {
        private readonly ProjectMobileContext _context;

        public TblToolsController(ProjectMobileContext context)
        {
            _context = context;
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

        // PUT: api/TblTools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblTool(int id, TblTool tblTool)
        {
            if (id != tblTool.Id)
            {
                return BadRequest();
            }

            _context.Entry(tblTool).State = EntityState.Modified;

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

            return NoContent();
        }

        // POST: api/TblTools
        [HttpPost]
        public async Task<ActionResult<TblTool>> PostTblTool(TblTool tblTool)
        {
            _context.TblTool.Add(tblTool);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblTool", new { id = tblTool.Id }, tblTool);
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