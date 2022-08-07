using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ActivityContent
    {
        public int ActivityId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public virtual ActivityOutline ActivityOutline { get; set; } = null!;
    }
}
