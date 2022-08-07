using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ArticlePicture
    {
        public ArticlePicture()
        {
            ArticleContents = new HashSet<ArticleContent>();
        }

        public int Id { get; set; }
        public string PictureUrl { get; set; } = null!;

        public virtual ICollection<ArticleContent> ArticleContents { get; set; }
    }
}
