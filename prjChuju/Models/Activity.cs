using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class Activity
    {
        public Activity()
        {
            ActivityImages = new HashSet<ActivityImage>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Thumbnail { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ActivityImage> ActivityImages { get; set; }
    }
}
