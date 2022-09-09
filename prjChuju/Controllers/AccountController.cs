using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth;
using prjChuju.Models;
using System.Security.Cryptography;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.EntityFrameworkCore;

namespace prjChuju.Controllers
{
    public class AccountController : Controller
    {
        private readonly dbChujuContext _dbChujuContext;

        private readonly IConfiguration _configuration;
        public AccountController(dbChujuContext dbChujuContext, IConfiguration configuration)
        {
            _dbChujuContext = dbChujuContext;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(string token)
        {
            var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { "877790294169-pnpcg04081hnbrp8ke2p5c99q105ccuc.apps.googleusercontent.com" }
            });

            var item = await _dbChujuContext.Accounts.FirstOrDefaultAsync(x => x.Email == googleUser.Email);

            if (item == null)
            {
                return BadRequest();
            }

            var jwtToken = CreateJWT(googleUser.Email, item.Role);
            Response.Cookies.Append("jwtToken", jwtToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, Secure = true, Expires = DateTime.Now.AddHours(1) });

            return Ok();
        }

        public async Task<int> Register(string token)
        {
            int num = 0;
            var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { "877790294169-pnpcg04081hnbrp8ke2p5c99q105ccuc.apps.googleusercontent.com" }
            });

            var item = await _dbChujuContext.Accounts.FirstOrDefaultAsync(x => x.Email == googleUser.Email);
            if (item == null)
            {
                item = new Account
                {
                    Email = googleUser.Email,
                    Role = 2
                };

                _dbChujuContext.Accounts.Add(item);
                num = _dbChujuContext.SaveChanges();
            }

            return num;
        }

        private string CreateJWT(string email, int role)
        {
            var rsa = RSA.Create();
            rsa.FromXmlString(_configuration["JWT:privateKey"]);

            var token = JwtBuilder.Create()
                      .WithAlgorithm(new RS256Algorithm(rsa, rsa))
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim("email", email)
                      .AddClaim("role", role)
                      .AddClaim("iss", _configuration["JWT:iss"])
                      .AddClaim("aud", _configuration["JWT:aud"])
                      .Encode();

            return token;
        }
    }
}
