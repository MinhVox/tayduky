using Microsoft.Extensions.Configuration;
using ProjectMobileAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMobileAPI.Repositories
{
    public class ActorRepository
    {
        private readonly ProjectMobileContext _context;
        private readonly IConfiguration _config;

        public ActorRepository(ProjectMobileContext context)
        {
            this._context = context;
        }

        public List<string> GetAvailableActorID(int id)
        {
            List<string> listActor = new List<string>();
            var result = from actor in _context.TblAccount
                         where actor.Role == 0 
                         where !(from sa in _context.TblSceneActor
                                 where sa.Idscene == id
                                 select sa.Username)
                                 .Contains(actor.Username)
                        select actor.Username;
            foreach (var actor in result) listActor.Add(actor);
            return listActor;
        }

        public List<TblActor> GetAvailableActor(int id)
        {
            List<TblActor> list = new List<TblActor>();
            var result = GetAvailableActorID(id);
            foreach(var idAc in result)
            {
               var actor = _context.TblActor
                    .Where(i => i.Username == idAc)
                    .FirstOrDefault();
                list.Add(actor);
            }
            return list;
        }

        public List<int> GetIdSceneJoined(string username)
        {
            List<int> listIdScene = new List<int>();
            var result = from scene in _context.TblSceneActor
                         where scene.Username == username
                         select scene.Idscene;
            foreach (var id in result) listIdScene.Add(id);
            return listIdScene;
        }

        // 0 : chưa quay;
        // 1 : đã quay;
        public List<TblScene> GetSceneByTime(string username,int status)
        {
            List<TblScene> listScene = new List<TblScene>();
            var listId = GetIdSceneJoined(username);
            foreach (var id in listId)
            {
                var scene = _context.TblScene
                     .Where(i => i.Id == id)
                     .FirstOrDefault();
                if(status == 0)
                {
                    var checkDate = DateTime.Compare(DateTime.Now, (DateTime)scene.EndDay);
                    if(checkDate > 0)
                    {
                        listScene.Add(scene);
                    }
                }
                else
                {
                    var checkDate = DateTime.Compare(DateTime.Now, (DateTime)scene.StartDay);
                    if (checkDate < 0)
                    {
                        listScene.Add(scene);
                    }
                }
            }
            return listScene;
        }
    }
}
