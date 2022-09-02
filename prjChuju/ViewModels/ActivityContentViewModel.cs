using prjChuju.Models;

namespace prjChuju.ViewModels
{
    public class ActivityContentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
