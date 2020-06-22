using System;
using System.Collections.Generic;

namespace ProjectMobileAPI.Models
{
    public partial class TblSceneTool
    {
        public int Idscene { get; set; }
        public int Idtool { get; set; }
        public int? Amount { get; set; }

        public virtual TblScene IdsceneNavigation { get; set; }
        public virtual TblTool IdtoolNavigation { get; set; }
    }
}
