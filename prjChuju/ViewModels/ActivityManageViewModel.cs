using prjChuju.Models;

namespace prjChuju.ViewModels
{
    public class ActivityManageViewModel : ActivityViewModel
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string ModifiedDate { get; set; } = null!;
    }
}
