using Microsoft.EntityFrameworkCore;
using MyAPI3.Models;

namespace MyAPI3.DBContext
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions o) : base(o) { }

        #region DbSet
        public DbSet<User> Users { get; set; }
        #endregion
    }
}
