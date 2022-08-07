using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class ViewedBuilding
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int BuildingId { get; set; }
        public DateTime? ViewDate { get; set; }

        public virtual AccountInfo Account { get; set; } = null!;
        public virtual Buildingdb Building { get; set; } = null!;
    }
}
