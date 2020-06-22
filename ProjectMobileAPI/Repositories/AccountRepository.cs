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
                .Where(record => record.Username == account.Username && record.Password == account.Password)
                .FirstOrDefault();
            return acc;
        }
    }
}
