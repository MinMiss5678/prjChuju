using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class CollectArticle
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int ArticleId { get; set; }
        public DateTime? CollectDate { get; set; }

        public virtual AccountInfo Account { get; set; } = null!;
        public virtual ArticleOutline Article { get; set; } = null!;
    }
}
