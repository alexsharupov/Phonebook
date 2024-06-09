using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database.Db
{
    public class ApplicationContext : DbContext
    {


        public DbSet<RandomUserEntity> Users => Set<RandomUserEntity>();
        public DbSet<LoginEntity> Logins => Set<LoginEntity>();
        public DbSet<PictureEntity> Pictures => Set<PictureEntity>();
        public DbSet<NameEntity> Name => Set<NameEntity>();
        public DbSet<RegisteredEntity> Registered => Set<RegisteredEntity>();

        public ApplicationContext()
        {
        //    Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "phoneList.db");
            optionsBuilder
                .UseLazyLoadingProxies()        // подключение lazy loading
                .UseSqlite($"Data Source={sqlitePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RandomUserEntity>().HasKey(u => u.userid).HasName("PK_RandomUser");
            modelBuilder.Entity<LoginEntity>().HasKey(u => u.userid).HasName("PK_Login");
            modelBuilder.Entity<PictureEntity>().HasKey(u => u.userid).HasName("PK_Picture");
            modelBuilder.Entity<NameEntity>().HasKey(u => u.userid).HasName("PK_Name");
            modelBuilder.Entity<RegisteredEntity>().HasKey(u => u.userid).HasName("PK_Registered");
        }
    }
}
