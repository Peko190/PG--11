using Microsoft.EntityFrameworkCore;
using PG_Тема_11.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG_Тема_11.Infrastructure.EFData_Sql
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=desktop-65gmhv2;Database=PGTEMA;Trusted_Connection=True;TrustServerCertificate=True;");
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>(builder =>
            {
                builder.Property(x => x.level)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                builder.HasMany(c => c.Enrolments)
                    .WithOne(e => e.Course)
                    .HasForeignKey(e => e.CourseId);
            });

            modelBuilder.Entity<Enrolments>(builder =>
            {
                builder.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasMaxLength(50)
                    .IsRequired();

                builder.Property(e => e.Progress)
                    .IsRequired();

                builder.HasOne(e => e.Student)
                    .WithMany()
                    .HasForeignKey(e => e.StudentId);
            });
          }
            public DbSet<Courses> Courses { get; set; }
          public DbSet<Enrolments> Enrolments { get; set; }
    } 
    
}
