using prjChuju.Models;

namespace prjChuju.ViewModels
{
    public class ActivityViewModel
    {
        private dbChujuContext db;

        public ActivityViewModel(dbChujuContext context)
        {
            db = context;
        }

        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Picture { get; set; } = null!;

        public virtual ActivityContent IdNavigation { get; set; } = null!;
    }
}
