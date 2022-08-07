using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ArticleOutline
    {
        public ArticleOutline()
        {
            ArticleContents = new HashSet<ArticleContent>();
            CollectArticles = new HashSet<CollectArticle>();
            ViewedArticles = new HashSet<ViewedArticle>();
            Tags = new HashSet<ArticleTagList>();
        }

        public int Id { get; set; }
        public int ArticleClass { get; set; }
        public DateTime PublishDate { get; set; }
        public string Title { get; set; } = null!;
        public string Intro { get; set; } = null!;
        public string ArticleSource { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;

        public virtual ArticleClass ArticleClassNavigation { get; set; } = null!;
        public virtual ICollection<ArticleContent> ArticleContents { get; set; }
        public virtual ICollection<CollectArticle> CollectArticles { get; set; }
        public virtual ICollection<ViewedArticle> ViewedArticles { get; set; }

        public virtual ICollection<ArticleTagList> Tags { get; set; }
    }
}
