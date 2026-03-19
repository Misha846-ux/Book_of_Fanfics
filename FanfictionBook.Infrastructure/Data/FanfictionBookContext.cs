using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanfictionBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FanfictionBook.Infrastructure.Data
{
    public class FanfictionBookContext: DbContext
    {
        public DbSet<CommentsEntity> Comments { get; set; }
        public DbSet<FanficEntity> Fanfics { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public FanfictionBookContext(DbContextOptions<FanfictionBookContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique();

            if (Database.IsSqlServer())
            {
                modelBuilder.Entity<FanficEntity>()
                    .Property(b => b.CreatedAt)
                    .HasDefaultValueSql("SYSDATETIME()");
            }


            // Пользователь -> Фанфики (Cascade)
            modelBuilder.Entity<FanficEntity>()
                .HasOne(f => f.User)
                .WithMany(u => u.Fanfics)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Фанфик -> Комментарии (Cascade)
            modelBuilder.Entity<CommentsEntity>()
                .HasOne(c => c.Fanfic)
                .WithMany(f => f.Comments)
                .HasForeignKey(c => c.FanficId)
                .OnDelete(DeleteBehavior.Cascade);

            // Комментарий -> Пользователь (Restrict/NoAction)
            modelBuilder.Entity<CommentsEntity>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction); // или Restrict
        }
    }
}
