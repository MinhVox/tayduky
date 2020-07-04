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

        public bool AddNewScene(TblScene scene)
        {

            var t = _context.TblScene.Add(new TblScene()
            {
                Name = scene.Name,
                NumberOfShotScenes = scene.NumberOfShotScenes,
                Description = scene.Description,
                StartDay = scene.StartDay,
                EndDay = scene.EndDay,
                Director = scene.Director,
                FileDocOfRole = scene.FileDocOfRole,
                Createtime = DateTime.Now,
                Lastmodified = DateTime.Now
            });
            _context.SaveChanges();
            return true;
        }
    }
}
