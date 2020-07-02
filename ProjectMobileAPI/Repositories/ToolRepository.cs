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

        public bool AddNewTool(String username, TblTool tool)
        {

            var t = _context.TblTool.Add(new TblTool()
            {
                Name = tool.Name,
                Amount = tool.Amount,
                Description = tool.Description,
                Img = tool.Img,
                Status = true,
                Username = username,
                Createtime = DateTime.Now,
                LastModified = DateTime.Now
            });
            _context.SaveChanges();
            return true;
        }
    }
}
