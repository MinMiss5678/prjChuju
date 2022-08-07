using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class AccountPicture
    {
        public AccountPicture()
        {
            AccountInfos = new HashSet<AccountInfo>();
        }

        public int Id { get; set; }
        public string PictureUrl { get; set; } = null!;
        public int? Uploader { get; set; }
        public DateTime? UploadDate { get; set; }

        public virtual ICollection<AccountInfo> AccountInfos { get; set; }
    }
}
