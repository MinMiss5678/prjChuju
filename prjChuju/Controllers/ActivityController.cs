using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjChuju.Models;
using prjChuju.ViewModels;

namespace prjChuju.Controllers
{
    public class ActivityController : Controller
    {
        private readonly dbChujuContext dbChujuContext;

        public ActivityController(dbChujuContext context)
        {
            dbChujuContext = context;
        }
        [Route("Activity/Index/{pageIndex?}")]
        public async Task<IActionResult> Index(int? pageIndex)
        {
            int pageSize = 4;
            //PaginatedList<ActivityOutline> page = new PaginatedList<ActivityOutline>(dbChujuContext.ActivityOutlines.ToList(), dbChujuContext.ActivityOutlines.Count(), pageIndex, 4);
            //var one = db.ActivityOutlines.Select(x => x);
            //db.SaveChanges();
            return View(await PaginatedList<ActivityOutline>.CreateAsync(dbChujuContext.ActivityOutlines.AsNoTracking(), pageIndex ?? 1, pageSize));
        }

        public class PaginatedList<T> : List<T>
        {
            public int PageIndex { get; private set; }
            public int TotalPage { get; private set; }

            public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
            {
                PageIndex = pageIndex;
                TotalPage = (int)Math.Ceiling(count / (double)pageSize);

                this.AddRange(items);
            }

            public bool HasPreviousPage => PageIndex > 1;

            public bool HasNextPage => PageIndex < TotalPage;

            public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
            {
                var count = await source.CountAsync();
                var item = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                return new PaginatedList<T>(item, count, pageIndex, pageSize);
            }
        }
    }
}
