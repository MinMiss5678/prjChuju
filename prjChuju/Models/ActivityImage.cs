using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ActivityImage
    {
        public int Id { get; set; }
        public string Image { get; set; } = null!;
        public int ActivityId { get; set; }

        public virtual Activity Activity { get; set; } = null!;
    }
}
