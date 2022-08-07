using System;
using System.Collections.Generic;

namespace prjChuju.Models
{
    public partial class Buildingdb
    {
        public Buildingdb()
        {
            BookedCases = new HashSet<BookedCase>();
            CollectBuildings = new HashSet<CollectBuilding>();
            ViewedBuildings = new HashSet<ViewedBuilding>();
        }

        public int 建案序號 { get; set; }
        public string? 縣市 { get; set; }
        public string? 地區 { get; set; }
        public string? 建案名稱 { get; set; }
        public string? 營造公司 { get; set; }
        public string? 坪數規劃 { get; set; }
        public string? 樓層規劃 { get; set; }
        public string? 基地面積 { get; set; }
        public string? 基地位置 { get; set; }
        public string? 接待中心 { get; set; }
        public string? 諮詢專線 { get; set; }
        public string? 特點 { get; set; }
        public string? 現況 { get; set; }
        public string? PicUrl { get; set; }
        public string? 房型規劃 { get; set; }

        public virtual ICollection<BookedCase> BookedCases { get; set; }
        public virtual ICollection<CollectBuilding> CollectBuildings { get; set; }
        public virtual ICollection<ViewedBuilding> ViewedBuildings { get; set; }
    }
}
