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
                    @"Server = (localdb)\MSSQLLocalDB; Database = PGTEMA; Trusted_Connection = True; TrustServerCertificate = True; "
);
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Courses>(builder =>
            {
                builder.HasKey(c => c.Id);
                builder.Property(c => c.Title).HasMaxLength(200).IsRequired();
                builder.Property(c => c.Description).IsRequired();
                builder.Property(c => c.level).HasConversion<string>().HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Students>(builder =>
            {
                builder.HasKey(s => s.Id);
                builder.Property(s => s.FirstName).HasMaxLength(100).IsRequired();
                builder.Property(s => s.LastName).HasMaxLength(100).IsRequired();
                builder.Property(s => s.Email).HasMaxLength(150).IsRequired();
            });

            modelBuilder.Entity<Enrolments>(builder =>
            {
                builder.HasKey(e => e.Id);
                builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(50).IsRequired();
                builder.Property(e => e.Progress).IsRequired();
                builder.Property(e => e.EnrolmentDate).IsRequired();

                builder.HasOne(e => e.Course).WithMany(c => c.Enrolments).HasForeignKey(e => e.CourseId);
                builder.HasOne(e => e.Student).WithMany(s => s.Enrolments).HasForeignKey(e => e.StudentId);
            });

            modelBuilder.Entity<Lessons>(builder =>
            {
                builder.HasKey(l => l.Id);
                builder.Property(l => l.Title).HasMaxLength(200).IsRequired();
                builder.Property(l => l.Content).IsRequired();
                builder.Property(l => l.Order).IsRequired();

                builder.HasOne(l => l.Courses).WithMany().HasForeignKey(l => l.CourseId);
                builder.HasMany(l => l.Tests).WithOne().HasForeignKey(t => t.LessonId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Tests>(builder =>
            {
                builder.HasKey(t => t.Id);
                builder.Property(t => t.Title).HasMaxLength(200).IsRequired();
                builder.Property(t => t.CourseId).IsRequired(false);
                builder.Property(t => t.LessonId).IsRequired(false);

                
                builder.HasOne<Lessons>()
                       .WithMany(l => l.Tests)
                       .HasForeignKey(t => t.LessonId)
                       .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne<Courses>()
                       .WithMany()
                       .HasForeignKey(t => t.CourseId)
                       .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TestQuestions>(builder =>
            {
                builder.HasKey(q => q.Id);
                builder.Property(q => q.QuestionText).IsRequired();
                builder.Property(q => q.CorrectAnswer).HasMaxLength(5).IsRequired();

                builder.HasOne<Tests>()
                       .WithMany()
                       .HasForeignKey(q => q.TestId)
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentTestResults>(builder =>
            {
                builder.HasKey(r => r.Id);
                builder.Property(r => r.Score).IsRequired();

                builder.HasOne<Students>().WithMany().HasForeignKey(r => r.StudentId);
                builder.HasOne<Tests>().WithMany().HasForeignKey(r => r.TestId);
            });

            modelBuilder.Entity<Progress>(builder =>
            {
                builder.ToTable("Progress");

                builder.HasKey(p => p.Id);

                builder.Property(p => p.StudentId).IsRequired();
                builder.Property(p => p.LessonId).IsRequired();

                builder.HasOne<Students>()
                       .WithMany()
                       .HasForeignKey(p => p.StudentId)
                       .HasConstraintName("FK_Progress_Students_StudentId")
                       .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne<Lessons>()
                       .WithMany()
                       .HasForeignKey(p => p.LessonId)
                       .HasConstraintName("FK_Progress_Lessons_LessonId")
                       .OnDelete(DeleteBehavior.Cascade);
            });
        }


        public DbSet<Courses> Courses { get; set; }
          public DbSet<Enrolments> Enrolments { get; set; }
          public DbSet<Lessons> Lessons { get; set; }
          public DbSet<Students> Students { get; set; }
          public DbSet<Tests> Tests{ get; set; }
          public DbSet<TestQuestions> TestQuestions{ get; set; }
          public DbSet<StudentTestResults> StudentTestResults{ get; set; }
          public DbSet<Progress> Progress { get; set; }
    } 
    
}
