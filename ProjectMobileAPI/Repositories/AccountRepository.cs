using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectMobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMobileAPI.Repositories
{

    public class AccountRepository
    {
        private readonly ProjectMobileContext _context;
        private readonly IConfiguration _config;

        public AccountRepository(ProjectMobileContext context)
        {
            this._context = context;
        }


        public TblAccount CheckLogin(TblAccount account)
        {
            TblAccount acc = _context.TblAccount
                .Where(record => record.Username == account.Username && record.Password == account.Password && record.Status == true)
                .Include(actor => actor.TblActor)
                .FirstOrDefault();
            return acc;
        }

        public bool changeStatus(TblAccount account)
        {
            var acc = _context.TblAccount.Where(record => record.Username == account.Username).FirstOrDefault();
            if(acc == null)
            {
                return false;
            }
            acc.Status = account.Status;
            _context.SaveChanges();
            return true;
        }

        public bool AddNewActor(TblActor account)
        {
            var acc = _context.TblAccount.Add(new TblAccount()
            {
                Username = account.Username,
                Password = "1",
                Role = 0,
                Status = true,
            });

            var actor = _context.TblActor.Add(new TblActor()
            {
                Username = account.Username,
                Name = account.Name,
                Email = account.Email,
                Phone = account.Phone,
                Description = account.Description,
                Img = account.Img,
                Createtime = DateTime.Now,
                Lastmodified = DateTime.Now
            });
            _context.SaveChanges();
            return true;
        }
    }
}
