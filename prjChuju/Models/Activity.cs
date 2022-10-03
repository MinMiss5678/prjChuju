using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ThumbnailId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

        public virtual ActivityImage Thumbnail { get; set; } = null!;
    }
}
