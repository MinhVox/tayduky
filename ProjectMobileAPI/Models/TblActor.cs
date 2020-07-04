using System;
using System.Collections.Generic;

namespace ProjectMobileAPI.Models
{
    public partial class TblActor
    {
        public TblActor()
        {
            TblSceneActor = new HashSet<TblSceneActor>();
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? Createtime { get; set; }
        public DateTime? Lastmodified { get; set; }
        public bool? Status { get; set; }

        public virtual TblAccount UsernameNavigation { get; set; }
        public virtual ICollection<TblSceneActor> TblSceneActor { get; set; }
    }
}
