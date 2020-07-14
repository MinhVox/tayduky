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
    public class AccountsController : ControllerBase
    {
        private readonly ProjectMobileContext _context;
        private readonly AccountRepository _accRepo;

        public AccountsController(ProjectMobileContext context)
        {
            _context = context;
            _accRepo = new AccountRepository(_context);
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult> GetAccountWithRoleActor()
        {
            var result = await _context.TblAccount
                .Where(i => i.Role == 0)
                .Select(a => new { 
                    role = a.Role.ToString(),
                    username = a.Username,
                    status = a.Status,
                    actor = a.TblActor
                })
                .ToListAsync();
            if (result != null){
                return Ok(result);
            }
            return Ok(null);
        }

        //loginAPI
        [HttpPost("/api/login")]
        public ActionResult CheckLogin(TblAccount account)
        {
            var result = _accRepo.CheckLogin(account);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPut("/api/Accounts/status")]
        public IActionResult ChangeStatus(TblAccount account)
        {
            var result = _accRepo.changeStatus(account);
            if (result == true)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("/api/Accounts")]
        public IActionResult AddNewAccount(TblActor account)
        {
            try
            {
                var result = _accRepo.AddNewActor(account);
                if (result == true)
                {
                    return Ok(result);
                }
            }
            catch(DbUpdateException)
            {
                if (AccountExists(account.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return BadRequest();
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAccount>> GetAccount(string id)
        {
            var account = await _context.TblAccount.FindAsync(id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}/{oldPassword}")]
        public async Task<IActionResult> PutAccount(string id, string oldPassword, TblAccount account)
        {
            if (id != account.Username)
            {
                return BadRequest();
            }

            var acc = _context.TblAccount.Where(ac => ac.Username == id).FirstOrDefault();
            if (acc != null)
            {
               if(oldPassword == acc.Password)
                {
                    acc.Password = account.Password;
                }
                else
                {
                    return BadRequest("Wrong old password");
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TblAccount>> DeleteAccount(string id)
        {
            var account = await _context.TblAccount.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.TblAccount.Remove(account);
            await _context.SaveChangesAsync();

            return account;
        }

        private bool AccountExists(string id)
        {
            return _context.TblAccount.Any(e => e.Username == id);
        }
    }
}
