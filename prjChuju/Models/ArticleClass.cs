using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ArticleClass
    {
        public ArticleClass()
        {
            ArticleOutlines = new HashSet<ArticleOutline>();
        }

        public int Id { get; set; }
        public string MainClass { get; set; } = null!;
        public string ClassName { get; set; } = null!;

        public virtual ICollection<ArticleOutline> ArticleOutlines { get; set; }
    }
}
