using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Db
{
    public class ApplicationContext : DbContext
    {


        public DbSet<RandomUserEntity> Users => Set<RandomUserEntity>();
        public DbSet<LoginEntity> Logins => Set<LoginEntity>();

        public ApplicationContext()
        {
            //      Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "phoneList.db");
            optionsBuilder.UseSqlite($"Data Source={sqlitePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RandomUserEntity>().HasKey(u => u.userid).HasName("PK_RandomUser");
            modelBuilder.Entity<LoginEntity>().HasKey(u => u.userid).HasName("PK_Login");
        }
    }
}
