using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class BookedCase
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int BuildingId { get; set; }
        public DateTime? BookedDate { get; set; }
        public DateTime? ClickDate { get; set; }

        public virtual AccountInfo Account { get; set; } = null!;
        public virtual Buildingdb Building { get; set; } = null!;
    }
}
