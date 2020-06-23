using System;
using System.Collections.Generic;

namespace ProjectMobileAPI.Models
{
    public partial class TblAccount
    {
        public TblAccount()
        {
            TblScene = new HashSet<TblScene>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool Status { get; set; }

        public virtual TblActor TblActor { get; set; }
        public virtual ICollection<TblScene> TblScene { get; set; }
    }
}
