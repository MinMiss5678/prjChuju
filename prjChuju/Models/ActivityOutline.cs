using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ActivityOutline
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Picture { get; set; } = null!;

        public virtual ActivityContent IdNavigation { get; set; } = null!;
    }
}
