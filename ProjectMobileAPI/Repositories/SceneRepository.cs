using Microsoft.Extensions.Configuration;
using ProjectMobileAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMobileAPI.Repositories
{
    public class SceneRepository
    {
        private readonly ProjectMobileContext _context;
        private readonly IConfiguration _config;

        public SceneRepository(ProjectMobileContext context)
        {
            this._context = context;
        }
    }
}
