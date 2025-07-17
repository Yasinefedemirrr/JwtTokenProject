using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JWT.Application.Interfaces;
using JWT.Domain.Entity;
using Persistance.Context;

namespace Persistance.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly JwtContext _context;
        public AppUserRepository(JwtContext context)
        {
            _context = context;
        }
        public Task<List<AppUser>> GetByFilterAsync(Expression<Func<AppUser, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
