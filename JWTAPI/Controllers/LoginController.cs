using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JWT.Application.Tools;
using JWT.Domain.Entity;
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
            return Ok(token);
        }
    }
}