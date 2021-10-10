using System;
using System.Collections.Generic;

#nullable disable

namespace EcoCareServerBL.Models
{
    public partial class Goal
    {
        public DateTime DateT { get; set; }
        public int Goal1 { get; set; }
        public string UserName { get; set; }

        public virtual RegularUser UserNameNavigation { get; set; }
    }
}
