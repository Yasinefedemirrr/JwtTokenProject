using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JWT.Application.Tools;
using Persistance.Context;
using JWT.Application.Dtos;
using JWT.Application.Features.CQRS.Results.AppUserResults;

namespace JWTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtContext _context;

        public LoginController(JwtContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _context.AppUsers
                .Include(x => x.AppRole)
                .FirstOrDefaultAsync(x => x.Username == dto.Username && x.Password == dto.Password);

            if (user == null)
                return BadRequest("Kullanıcı adı veya şifre hatalıdır.");

            var result = new GetCheckAppUserQueryResult
            {
                Id = user.AppUserId,
                Username = user.Username,
                Role = user.AppRole?.AppRoleName ?? "User"
            };

            var token = JwtTokenGenerator.GenerateToken(result);

            // Refresh Token'ı veritabanına kaydet
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.RefreshTokenExpireDate;
            await _context.SaveChangesAsync();

            // Access Token Cookie
            Response.Cookies.Append("JWTToken", token.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = token.ExpireDate
            });

            // Refresh Token Cookie
            Response.Cookies.Append("RefreshToken", token.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = token.RefreshTokenExpireDate
            });

            return Ok(token);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("Refresh token yok");

            var user = await _context.AppUsers.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpireDate < DateTime.UtcNow)
                return Unauthorized("Refresh token geçersiz");

            var result = new GetCheckAppUserQueryResult
            {
                Id = user.AppUserId,
                Username = user.Username,
                Role = user.AppRole?.AppRoleName ?? "User"
            };

            var newToken = JwtTokenGenerator.GenerateToken(result);

            // Yeni Refresh Token güncelle
            user.RefreshToken = newToken.RefreshToken;
            user.RefreshTokenExpireDate = newToken.RefreshTokenExpireDate;
            await _context.SaveChangesAsync();

            // Access Token Cookie yenile
            Response.Cookies.Append("JWTToken", newToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = newToken.ExpireDate
            });

            // Refresh Token Cookie yenile
            Response.Cookies.Append("RefreshToken", newToken.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = newToken.RefreshTokenExpireDate
            });

            return Ok(newToken);
        }
    }
}
