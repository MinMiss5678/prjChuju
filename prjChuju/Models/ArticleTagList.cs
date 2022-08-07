using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ArticleTagList
    {
        public ArticleTagList()
        {
            Articles = new HashSet<ArticleOutline>();
        }

        public int Id { get; set; }
        public string TagName { get; set; } = null!;

        public virtual ICollection<ArticleOutline> Articles { get; set; }
    }
}
