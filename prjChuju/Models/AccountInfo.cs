using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class AccountInfo
    {
        public AccountInfo()
        {
            BookedCases = new HashSet<BookedCase>();
            CollectArticles = new HashSet<CollectArticle>();
            CollectBuildings = new HashSet<CollectBuilding>();
            ForgetCodes = new HashSet<ForgetCode>();
            ViewedArticles = new HashSet<ViewedArticle>();
            ViewedBuildings = new HashSet<ViewedBuilding>();
        }

        public int Id { get; set; }
        public string Permission { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime? Birthday { get; set; }
        public string? Gender { get; set; }
        public string? Cellphone { get; set; }
        public int? Region { get; set; }
        public int? Avatar { get; set; }

        public virtual AccountPicture? AvatarNavigation { get; set; }
        public virtual RegionList? RegionNavigation { get; set; }
        public virtual ICollection<BookedCase> BookedCases { get; set; }
        public virtual ICollection<CollectArticle> CollectArticles { get; set; }
        public virtual ICollection<CollectBuilding> CollectBuildings { get; set; }
        public virtual ICollection<ForgetCode> ForgetCodes { get; set; }
        public virtual ICollection<ViewedArticle> ViewedArticles { get; set; }
        public virtual ICollection<ViewedBuilding> ViewedBuildings { get; set; }
    }
}
