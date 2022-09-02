using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjChuju.Models;
using prjChuju.ViewModels;

namespace prjChuju.Controllers
{
    public class ActivityController : Controller
    {
        private readonly dbChujuContext _dbChujuContext;

        public ActivityController(dbChujuContext context)
        {
            _dbChujuContext = context;
        }


        public IActionResult Index()
        {
            List<ActivityViewModel> vmodelList = new List<ActivityViewModel>();
            int pageSize = 4;
            var item = _dbChujuContext.Activities.AsNoTracking().OrderByDescending(x => x.EndDate).Take(pageSize)
                .Select(x => new ActivityViewModel
                {
                    Id = x.Id,
                    StartDate = x.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                    Thumbnail = x.Thumbnail,
                    Tag = SetTag(x),
                });

            return View(item);
        }

        static private string SetTag(Activity item)
        {
            string tag;

            if (DateTime.Now.Date >= item.StartDate && DateTime.Now.Date <= item.EndDate)
            {
                tag = "執行中";
            }

            else if (DateTime.Now.Date < item.StartDate)
            {
                tag = "即將開始";
            }

            else
            {
                tag = "已結束";
            }

            return tag;
        }
        public IActionResult ListActivity()
        {
            var item = _dbChujuContext.Activities.AsNoTracking().OrderByDescending(x => x.EndDate)
                .Select(x => new ActivityViewModel
                {
                    Id = x.Id,
                    StartDate = x.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                    Thumbnail = x.Thumbnail,
                    Tag = SetTag(x),
                });

            return Json(item);
        }

        [Route("Activity/Content/{id}")]
        public IActionResult Content(int id, ActivityContentViewModel contentViewModel)
        {
            var item = _dbChujuContext.Activities.Find(id);

            if (item == null)
            {
                return View("NotFound");
            }

            contentViewModel.Id = item.Id;
            contentViewModel.Title = item.Title;
            contentViewModel.Content = item.Content;

            return View(contentViewModel);
        }

        [Route("Activity/OtherActivity/{id}")]
        public IActionResult OtherActivity(int id)
        {
            int count = 3;
            var data = _dbChujuContext.Activities.Find(id);

            if (data == null)
            {
                return View("NotFound");
            }

            var item1 = _dbChujuContext.Activities.Where(x => x.EndDate >= data.EndDate && x.Id != id).OrderBy(x => x.EndDate).Take(count);

            var item2 = _dbChujuContext.Activities.Where(x => x.EndDate < data.EndDate && x.Id != id).OrderByDescending(x => x.EndDate).Take(count);

            var union = item1.Union(item2).OrderByDescending(x => x.EndDate).Select(x => new ActivityViewModel
            {
                Id = x.Id,
                StartDate = x.StartDate.ToString("yyyy-MM-dd"),
                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                Thumbnail = x.Thumbnail,
            });

            return Json(union);
        }

        //public class PaginatedList<T> : List<T>
        //{
        //    public int _pageIndex { get; private set; }

        //    public int _totalPage { get; private set; }

        //    public int _pageSize { get; private set; }

        //    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        //    {
        //        _pageIndex = pageIndex;
        //        _pageSize = pageSize;
        //        _totalPage = (int)Math.Ceiling(count / (double)pageSize);

        //        this.AddRange(items);
        //    }

        //    public PaginatedList(List<T> items)
        //    {
        //        this.AddRange(items);
        //    }

        //    public bool _HasPreviousPage => _pageIndex > 1;

        //    public bool _HasNextPage => _pageIndex < _totalPage;

        //    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        //    {
        //        var count = await source.CountAsync();
        //        var item = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        //        return new PaginatedList<T>(item, count, pageIndex, pageSize);
        //    }

        //}
    }
}
