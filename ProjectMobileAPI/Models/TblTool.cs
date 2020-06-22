using System;
using System.Collections.Generic;

namespace ProjectMobileAPI.Models
{
    public partial class TblTool
    {
        public TblTool()
        {
            TblSceneTool = new HashSet<TblSceneTool>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int? Amount { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<TblSceneTool> TblSceneTool { get; set; }
    }
}
