using Microsoft.EntityFrameworkCore;
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

        public bool AddToolToScene(TblSceneTool st)
        {
            var tool = _context.TblTool.Where(a => a.Id == st.Idtool).FirstOrDefault();
            if(tool.Amount < st.Amount)
            {
                return false;
            }
            else
            {
                var exist = _context.TblSceneTool
                    .Where(a => a.Idscene == st.Idscene && a.Idtool == st.Idtool)
                    .FirstOrDefault();
                if (exist != null)
                {
                    var toolInStore = _context.TblTool
                        .Where(a => a.Id == st.Idtool).FirstOrDefault();
                    if (toolInStore != null)
                    {
                       toolInStore.Amount = toolInStore.Amount - st.Amount;
                       exist.Amount = exist.Amount + st.Amount;
                    }
                }
                else
                {
                    _context.TblSceneTool.Add(new TblSceneTool()
                    {
                        Idscene = st.Idscene,
                        Idtool = st.Idtool,
                        Amount = st.Amount
                    });
                }
                _context.SaveChanges();
                return true;
            }
        }
    }
}
