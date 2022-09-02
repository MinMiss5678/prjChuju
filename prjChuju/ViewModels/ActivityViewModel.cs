using Microsoft.EntityFrameworkCore;
using prjChuju.Models;
using System.ComponentModel;

namespace prjChuju.ViewModels
{
    public class ActivityViewModel
    {
        public int Id { get; set; }

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public string Tag { get; set; } = null!;

        public string Thumbnail { get; set; } = null!;
    }
}
