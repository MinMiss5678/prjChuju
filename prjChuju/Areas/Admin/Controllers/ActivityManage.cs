using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjChuju.Models;
using prjChuju.ViewModels;
using System.Security.Cryptography;
using System.Text;

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
            var item = _dbChujuContext.Activities.AsNoTracking().Include(x => x.Thumbnail).OrderByDescending(x => x.Id).Select(x => new ActivityManageViewModel
            {
                Id = x.Id,
                StartDate = x.StartDate.ToString("yyyy-MM-dd"),
                EndDate = x.EndDate.ToString("yyyy-MM-dd"),
                Thumbnail = x.Thumbnail.Path,
                Title = x.Title,
                ModifiedDate = x.ModifiedDate.ToString("yyyy-MM-dd")
            });

            return item;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _dbChujuContext.Activities.AsNoTracking().Include(x => x.Thumbnail).FirstOrDefault(x => x.Id == id);

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
                Thumbnail = item.Thumbnail.Path,
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
                    var item = _dbChujuContext.Activities.Include(x => x.Thumbnail).FirstOrDefault(x => x.Id == id);

                    if (item == null)
                    {
                        return num;
                    }

                    var oldThumbnailId = item.ThumbnailId;
                    var oldThumbnailPath = item.Thumbnail.Path;

                    string folderPath = "wwwroot/images/ActivityPictures/OutlinePictures/";
                    var baseUrl = Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                    string base64;

                    using (var ms = new MemoryStream())
                    {
                        await file.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        base64 = Convert.ToBase64String(fileBytes);
                    }

                    var hash = ConvertStringSHA256(base64);
                    var folder = CreatFolder1(baseUrl, hash);
                    var newFileName = hash + ext;
                    var newPath = Path.Combine(baseUrl, folder, newFileName);
                    var dbPath = $"images/ActivityPictures/OutlinePictures/{folder}/{newFileName}";

                    using (var stream = System.IO.File.Create(newPath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var newImagePath = _dbChujuContext.ActivityImages.FirstOrDefault(x => x.Path == dbPath);

                    if (newImagePath == null)
                    {
                        var item2 = new ActivityImage
                        {
                            Path = dbPath
                        };

                        _dbChujuContext.ActivityImages.Add(item2);
                        _dbChujuContext.SaveChanges();

                        item.Title = form["title"];
                        item.StartDate = DateTime.ParseExact(form["startDate"], "yyyy-MM-dd", null);
                        item.EndDate = DateTime.ParseExact(form["endDate"], "yyyy-MM-dd", null);
                        item.ThumbnailId = item2.Id;
                        item.Content = form["content"];
                        item.ModifiedDate = DateTime.Now;

                        num = _dbChujuContext.SaveChanges();
                    }

                    else
                    {
                        item.Title = form["title"];
                        item.StartDate = DateTime.ParseExact(form["startDate"], "yyyy-MM-dd", null);
                        item.EndDate = DateTime.ParseExact(form["endDate"], "yyyy-MM-dd", null);
                        item.ThumbnailId = newImagePath.Id;
                        item.Content = form["content"];
                        item.ModifiedDate = DateTime.Now;

                        num = _dbChujuContext.SaveChanges();
                    }

                    var oldOtherThumbnail = _dbChujuContext.Activities.FirstOrDefault(x => x.ThumbnailId == oldThumbnailId);

                    if (oldOtherThumbnail == null)
                    {
                        var rootUrl = "wwwroot/";
                        var baseRootUrl = Path.Combine(_appEnvironment.ContentRootPath, rootUrl);
                        string oldPath = baseRootUrl + oldThumbnailPath;

                        System.IO.File.Delete(oldPath);

                        var oldThumbnail = _dbChujuContext.ActivityImages.FirstOrDefault(x => x.Id == oldThumbnailId);

                        if (oldThumbnail != null)
                        {
                            _dbChujuContext.ActivityImages.Remove(oldThumbnail);
                            _dbChujuContext.SaveChanges();
                        }
                    }
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
                string base64;

                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    base64 = Convert.ToBase64String(fileBytes);
                }

                var hash = ConvertStringSHA256(base64);
                var folder = CreatFolder1(baseUrl, hash);
                var newFileName = hash + ext;
                var newPath = Path.Combine(baseUrl, folder, newFileName);
                using (var stream = System.IO.File.Create(newPath))
                {
                    await file.CopyToAsync(stream);
                }

                var dbPath = $"images/ActivityPictures/OutlinePictures/{folder}/{newFileName}";

                var dbImagePath = _dbChujuContext.ActivityImages.FirstOrDefault(x => x.Path == dbPath);

                if (dbImagePath == null)
                {
                    var item = new ActivityImage
                    {
                        Path = dbPath
                    };

                    _dbChujuContext.ActivityImages.Add(item);
                    _dbChujuContext.SaveChanges();

                    var item2 = new Activity
                    {
                        Title = form["title"],
                        StartDate = DateTime.ParseExact(form["startDate"], "yyyy-MM-dd", null),
                        EndDate = DateTime.ParseExact(form["endDate"], "yyyy-MM-dd", null),
                        ThumbnailId = item.Id,
                        Content = form["content"],
                        ModifiedDate = DateTime.Now
                    };

                    _dbChujuContext.Activities.Add(item2);
                    num = _dbChujuContext.SaveChanges();
                }

                else
                {
                    var item2 = new Activity
                    {
                        Title = form["title"],
                        StartDate = DateTime.ParseExact(form["startDate"], "yyyy-MM-dd", null),
                        EndDate = DateTime.ParseExact(form["endDate"], "yyyy-MM-dd", null),
                        ThumbnailId = dbImagePath.Id,
                        Content = form["content"],
                        ModifiedDate = DateTime.Now
                    };

                    _dbChujuContext.Activities.Add(item2);
                    num = _dbChujuContext.SaveChanges();
                }
            }

            return num;
        }

        private DirectoryInfo CreatFolder(string root, string hash)
        {
            var header = hash[..2];
            var rootDir = Path.Combine(root, header);
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }
            var ser = hash[2..4];

            var path = Path.Combine(rootDir, ser);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return new DirectoryInfo(path);
        }

        private string CreatFolder1(string root, string hash)
        {
            var header = hash[..2];
            var rootDir = Path.Combine(root, header);
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }
            var ser = hash[2..4];

            var path = Path.Combine(rootDir, ser);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return $"{header}/{ser}";
        }

        private string ConvertStringSHA256(string strword)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(strword);
            byte[] hash = sha256.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            int num = 0;
            var item = _dbChujuContext.Activities.Include(x => x.Thumbnail).FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                string folderPath = "wwwroot/";
                var baseUrl = Path.Combine(_appEnvironment.ContentRootPath, folderPath);
                string filePath = item.Thumbnail.Path;
                string newPath = baseUrl + filePath;

                _dbChujuContext.Activities.Remove(item);
                num = _dbChujuContext.SaveChanges();
                var item2 = _dbChujuContext.Activities.FirstOrDefault(x => x.ThumbnailId == item.ThumbnailId);
                if (item2 == null)
                {
                    System.IO.File.Delete(newPath);
                }
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
