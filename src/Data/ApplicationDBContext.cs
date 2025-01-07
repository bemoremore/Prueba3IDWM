using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prueba3_Backend.src.Models;
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Prueba3_Backend.src.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserIdPost)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}