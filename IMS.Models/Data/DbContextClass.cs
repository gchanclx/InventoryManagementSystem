using IMS.Models.Models.Departments;
using Microsoft.EntityFrameworkCore;

namespace IMS.Models.Data
{
    public class DbContextClass : DbContext
    {
        private readonly DbContext _dbContext;
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        { }

        public DbSet<Department> Departments { get; set; }
    }
}
