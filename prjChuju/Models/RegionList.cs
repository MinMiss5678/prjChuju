using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class RegionList
    {
        public RegionList()
        {
            AccountInfos = new HashSet<AccountInfo>();
        }

        public int Id { get; set; }
        public string RegionName { get; set; } = null!;
        public int CityId { get; set; }

        public virtual CityList City { get; set; } = null!;
        public virtual ICollection<AccountInfo> AccountInfos { get; set; }
    }
}
