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

        public bool AddNewTool(TblTool tool)
        {

            var t = _context.TblTool.Add(new TblTool()
            {
                Name = tool.Name,
                Amount = tool.Amount,
                Description = tool.Description,
                Img = tool.Img,
                Status = true,
                Username = tool.Username,
                Createtime = DateTime.Now,
                LastModified = DateTime.Now
            });
            _context.SaveChanges();
            return true;
        }

        public List<int> GetAvailableToolID(int id)
        {
            List<int> listTool = new List<int>();
            var result = from tool in _context.TblTool
                         where !(from sa in _context.TblSceneTool
                                 where sa.Idscene == id
                                 select sa.Idtool)
                                 .Contains(tool.Id)
                         select tool.Id;
            foreach (var tool in result) listTool.Add(tool);
            return listTool;
        }

        public List<TblTool> GetAvailableTool(int id)
        {
            List<TblTool> list = new List<TblTool>();
            var result = GetAvailableToolID(id);
            foreach (var idTool in result)
            {
                var tool = _context.TblTool
                     .Where(i => i.Id == idTool)
                     .FirstOrDefault();
                list.Add(tool);
            }
            return list;
        }
    }
}
