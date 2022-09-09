using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjChuju.Models;
using prjChuju.ViewModels;

namespace prjChuju.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "1")]
    public class ActivityManage : ControllerBase
    {
        private readonly dbChujuContext _dbChujuContext;

        private readonly IWebHostEnvironment _appEnvironment;
       
        public ActivityManage(dbChujuContext context, IWebHostEnvironment appEnvironment)
        {
            _dbChujuContext = context;
            _appEnvironment = appEnvironment;
        }
       
        public IQueryable<ActivityManageViewModel> Get()
        {
            var item = _dbChujuContext.Activities.AsNoTracking().Select(x => new ActivityManageViewModel
            {
                Id = x.Id,
                StartDate = x.StartDate.ToString("yyyy-MM-dd"),
                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                Thumbnail = x.Thumbnail,
                Title = x.Title,
                ModifiedDate = x.ModifiedDate.ToString("yyyy-MM-dd")
            });

            return item;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _dbChujuContext.Activities.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            var vModel = new ActivityManageViewModel
            {
                Id = item.Id,
                Title = item.Title,
                StartDate = item.StartDate.ToString("yyyy-MM-dd"),
                EndDate = item.EndDate.ToString("yyyy-MM-dd"),
                Thumbnail = item.Thumbnail,
                Content = item.Content,
                ModifiedDate = item.ModifiedDate.ToString("yyyy-MM-dd")
            };


            return Ok(vModel);
        }

        [HttpPut("{id}")]
        public int Put(IFormCollection filterName, int id)
        {
            int num = 0;

            if (filterName.Files.Count != 0)
            {
                var file = filterName.Files[0];
                string folderPath = "wwwroot/images/ActivityPictures/OutlinePictures/";
                var baseUrl = Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                string fileName = file.FileName;
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string newPath = baseUrl + newFileName;

                using (var stream = System.IO.File.Create(newPath))
                {
                    file.CopyToAsync(stream);
                }

                var item = _dbChujuContext.Activities.FirstOrDefault(x => x.Id == id);

                if (item == null)
                {
                    return num;
                }

                item.Title = filterName["title"];
                item.StartDate = DateTime.ParseExact(filterName["startDate"], "yyyy-MM-dd", null);
                item.EndDate = DateTime.ParseExact(filterName["endDate"], "yyyy-MM-dd", null);
                item.Thumbnail = $"images/ActivityPictures/OutlinePictures/{newFileName}";
                item.Content = filterName["content"];
                item.ModifiedDate = DateTime.Now;

                num = _dbChujuContext.SaveChanges();
            }

            else
            {
                var item = _dbChujuContext.Activities.FirstOrDefault(x => x.Id == id);

                if (item == null)
                {
                    return num;
                }

                item.Title = filterName["title"];
                item.StartDate = DateTime.ParseExact(filterName["startDate"], "yyyy-MM-dd", null);
                item.EndDate = DateTime.ParseExact(filterName["endDate"], "yyyy-MM-dd", null);
                item.Content = filterName["content"];
                item.ModifiedDate = DateTime.Now;

                num = _dbChujuContext.SaveChanges();
            }

            return num;
        }

        [HttpPost]
        public int Post(IFormCollection filterName)
        {
            int num = 0;

            if (filterName.Files.Count != 0)
            {
                var file = filterName.Files[0];
                string folderPath = "wwwroot/images/ActivityPictures/OutlinePictures/";
                var baseUrl = Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                string fileName = file.FileName;
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string newPath = baseUrl + newFileName;

                using (var stream = System.IO.File.Create(newPath))
                {
                    file.CopyToAsync(stream);
                }

                var item = new Activity
                {
                    Title = filterName["title"],
                    StartDate = DateTime.ParseExact(filterName["startDate"], "yyyy-MM-dd", null),
                    EndDate = DateTime.ParseExact(filterName["endDate"], "yyyy-MM-dd", null),
                    Thumbnail = $"images/ActivityPictures/OutlinePictures/{newFileName}",
                    Content = filterName["content"],
                    ModifiedDate = DateTime.Now
                };

                _dbChujuContext.Activities.Add(item);
                num = _dbChujuContext.SaveChanges();
            }

            else
            {
                var item = new Activity
                {
                    Title = filterName["title"],
                    StartDate = DateTime.ParseExact(filterName["startDate"], "yyyy-MM-dd", null),
                    EndDate = DateTime.ParseExact(filterName["endDate"], "yyyy-MM-dd", null),
                    Thumbnail = $"images/ActivityPictures/OutlinePictures/",
                    Content = filterName["content"],
                    ModifiedDate = DateTime.Now
                };

                _dbChujuContext.Activities.Add(item);
                num = _dbChujuContext.SaveChanges();
            }
            return num;
        }


        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            int num = 0;
            var item = _dbChujuContext.Activities.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _dbChujuContext.Activities.Remove(item);
                num = _dbChujuContext.SaveChanges();
            }

            return num;
        }

        [HttpPost("ImageUpload")]
        public async Task<object> ImageUpload()
        {
            try
            {
                IFormFile file = HttpContext.Request.Form.Files[0];
                string folderPath = "wwwroot/images/ActivityPictures/ContentPictures/";
                var baseUrl = Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                int total;
                {
                    try
                    {
                        total = HttpContext.Request.Form.Files.Count;
                    }
                    catch (Exception ex)
                    {
                        return await Task.FromResult(new { error = new { message = ex } });
                    }

                    if (total == 0)
                    {
                        return await Task.FromResult(new { error = new { message = "no file has sent" } });
                    }

                    if (!Directory.Exists(baseUrl))
                    {
                        return await Task.FromResult(new { error = new { message = "Error does not exist" } });
                    }
                    string fileName = file.FileName;
                    string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                    string newPath = baseUrl + newFileName;

                    using (var stream = System.IO.File.Create(newPath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    string imageUrl = $"/images/ActivityPictures/ContentPictures/{newFileName}";

                    return await Task.FromResult(new { url = imageUrl });
                }
            }
            catch (Exception exception)
            {
                return await Task.FromResult(new { error = new { message = exception.Message } });
            }
        }
    }
}
