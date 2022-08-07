using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ArticleContent
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int ContentOrder { get; set; }
        public string ContentType { get; set; } = null!;
        public string? TextContent { get; set; }
        public int? PictureId { get; set; }

        public virtual ArticleOutline Article { get; set; } = null!;
        public virtual ArticlePicture? Picture { get; set; }
    }
}
