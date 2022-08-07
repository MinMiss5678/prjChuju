using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class CityList
    {
        public CityList()
        {
            RegionLists = new HashSet<RegionList>();
        }

        public int Id { get; set; }
        public string CityName { get; set; } = null!;

        public virtual ICollection<RegionList> RegionLists { get; set; }
    }
}
