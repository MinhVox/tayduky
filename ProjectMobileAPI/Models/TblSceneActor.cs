using System;
using System.Collections.Generic;

namespace ProjectMobileAPI.Models
{
    public partial class TblSceneActor
    {
        public int Idscene { get; set; }
        public string Username { get; set; }

        public virtual TblScene IdsceneNavigation { get; set; }
        public virtual TblActor UsernameNavigation { get; set; }
    }
}
