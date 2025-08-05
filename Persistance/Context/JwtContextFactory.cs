using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Context
{
    public class JwtContextFactory : IDesignTimeDbContextFactory<JwtContext>
    {
        public JwtContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<JwtContext>();
            optionsBuilder.UseSqlServer("Server=YASINEFEDEMIR\\SQLEXPRESS;Database=JwtProject;Trusted_Connection=True;TrustServerCertificate=True;");

            return new JwtContext(optionsBuilder.Options);
        }
    }
}
