using System;
using System.Collections.Generic;

namespace ProjectMobileAPI.Models
{
    public partial class TblScene
    {
        public TblScene()
        {
            TblSceneActor = new HashSet<TblSceneActor>();
            TblSceneTool = new HashSet<TblSceneTool>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public int? NumberOfShotScenes { get; set; }
        public string Director { get; set; }
        public string FileDocOfRole { get; set; }
        public string Createtime { get; set; }
        public string Lastmodified { get; set; }

        public virtual TblAccount DirectorNavigation { get; set; }
        public virtual ICollection<TblSceneActor> TblSceneActor { get; set; }
        public virtual ICollection<TblSceneTool> TblSceneTool { get; set; }
    }
}
