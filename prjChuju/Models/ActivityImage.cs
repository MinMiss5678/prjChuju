using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ActivityImage
    {
        public ActivityImage()
        {
            Activities = new HashSet<Activity>();
        }

        public int Id { get; set; }
        public string Path { get; set; } = null!;

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
