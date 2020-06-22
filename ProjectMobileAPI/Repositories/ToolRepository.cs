using Microsoft.Extensions.Configuration;
using ProjectMobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMobileAPI.Repositories
{
    public class ToolRepository
    {
        private readonly ProjectMobileContext _context;
        private readonly IConfiguration _config;

        public ToolRepository(ProjectMobileContext context)
        {
            _context = context;
        }
    }
}
