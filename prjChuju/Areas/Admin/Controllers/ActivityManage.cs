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
        public async Task<int> Put(IFormCollection form, int id)
        {
            int num = 0;

            string[] keyArray = { "title", "startDate", "endDate", "content" };

            if (form == null || form.Keys.Count > 5)
            {
                return num;
            }

            foreach (var key in keyArray)
            {
                if (!form.ContainsKey(key))
                {
                    return num;
                };
            }

            if (form.Files.Count == 1)
            {
                var file = form.Files[0];
                string ext = System.IO.Path.GetExtension(file.FileName);
                var size = file.Length / 1024 / 1024;

                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" && size <= 4)
                {
                    var item = _dbChujuContext.Activities.FirstOrDefault(x => x.Id == id);

                    if (item == null)
                    {
                        return num;
                    }

                    string folderPath = "wwwroot/images/ActivityPictures/OutlinePictures/";
                    var baseUrl = Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                    string fileName = file.FileName;
                    string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                    string newPath = baseUrl + newFileName;
                    var rootUrl = "wwwroot/";
                    var baseRootUrl = Path.Combine(_appEnvironment.ContentRootPath, rootUrl);
                    string oldPath = baseRootUrl + item.Thumbnail;

                    System.IO.File.Delete(oldPath);

                    using (var stream = System.IO.File.Create(newPath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    item.Title = form["title"];
                    item.StartDate = DateTime.ParseExact(form["startDate"], "yyyy-MM-dd", null);
                    item.EndDate = DateTime.ParseExact(form["endDate"], "yyyy-MM-dd", null);
                    item.Thumbnail = $"images/ActivityPictures/OutlinePictures/{newFileName}";
                    item.Content = form["content"];
                    item.ModifiedDate = DateTime.Now;

                    num = _dbChujuContext.SaveChanges();
                }
            }

            else
            {
                var item = _dbChujuContext.Activities.FirstOrDefault(x => x.Id == id);

                if (item == null)
                {
                    return num;
                }

                item.Title = form["title"];
                item.StartDate = DateTime.ParseExact(form["startDate"], "yyyy-MM-dd", null);
                item.EndDate = DateTime.ParseExact(form["endDate"], "yyyy-MM-dd", null);
                item.Content = form["content"];
                item.ModifiedDate = DateTime.Now;

                num = _dbChujuContext.SaveChanges();
            }

            return num;
        }

        [HttpPost]
        public async Task<int> Post(IFormCollection form)
        {
            int num = 0;

            string[] keyArray = { "title", "startDate", "endDate", "content" };

            if (form == null || form.Files.Count != 1 || form.Keys.Count != 4)
            {
                return num;
            }

            foreach (var key in keyArray)
            {
                if (!form.ContainsKey(key))
                {
                    return num;
                };
            }

            var file = form.Files[0];
            string ext = System.IO.Path.GetExtension(file.FileName);
            var size = file.Length / 1024 / 1024;

            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" && size <= 4)
            {
                string folderPath = "wwwroot/images/ActivityPictures/OutlinePictures/";
                var baseUrl = Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                string fileName = file.FileName;
                string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string newPath = baseUrl + newFileName;

                using (var stream = System.IO.File.Create(newPath))
                {
                    await file.CopyToAsync(stream);
                }

                var item = new Activity
                {
                    Title = form["title"],
                    StartDate = DateTime.ParseExact(form["startDate"], "yyyy-MM-dd", null),
                    EndDate = DateTime.ParseExact(form["endDate"], "yyyy-MM-dd", null),
                    Thumbnail = $"images/ActivityPictures/OutlinePictures/{newFileName}",
                    Content = form["content"],
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
                string folderPath = "wwwroot/";
                var baseUrl = Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                string filePath = item.Thumbnail;
                string newPath = baseUrl + filePath;
                System.IO.File.Delete(newPath);
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
