using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using static DAL.Entities.TrainingDAL;

namespace DAL
{
    public class DBContext : DbContext
    {
        public DbSet<UserRoleDAL> UserRoles { get; set; }
        public DbSet<UserDAL> Users { get; set; }
        public DbSet<RoleDAL> Roles { get; set; }
        public DbSet<CourseDAL> Courses { get; set; }
        public DbSet<LessonDAL> Lessons { get; set; }
        public DbSet<TrainingDAL> Trainings { get; set; }

        public DBContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DBContext([NotNullAttribute] DbContextOptions options, bool recreate) : base(options)
        {
            if (recreate)
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<UserDAL>()
            //.HasAlternateKey(u => u.Email); // bug in EF. update doesn't work https://github.com/dotnet/efcore/issues/4073
            modelBuilder.Entity<UserDAL>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<UserDAL>()
                .Property(r => r.Email).HasMaxLength(128);

            modelBuilder.Entity<UserRoleDAL>()
                .Property(p => p.RoleId)
                .IsRequired();
            modelBuilder.Entity<UserRoleDAL>()
                .Property(p => p.UserId)
                .IsRequired();
           modelBuilder.Entity<UserRoleDAL>()
                .HasOne(p => p.RoleDAL)
                .WithMany(r => r.UserRolesDAL)
                .HasForeignKey(p => p.RoleId)
                .HasConstraintName("FK_UR_Role");
            modelBuilder.Entity<UserRoleDAL>()
                .HasOne(p => p.UserDAL)
                .WithMany(r => r.UserRolesDAL)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_UR_User");

            modelBuilder.Entity<LessonDAL>()
                .Property(p => p.LectorID)
                .IsRequired();
            modelBuilder.Entity<LessonDAL>()
                .Property(p => p.CourseId)
                .IsRequired();
            modelBuilder.Entity<LessonDAL>()
                 .HasOne(p => p.CourseDAL)
                 .WithMany(r => r.LessonsDAL)
                 .HasForeignKey(p => p.CourseId)
                 .HasConstraintName("FK_Lesson_Role");
            modelBuilder.Entity<LessonDAL>()
                .HasOne(p => p.UserDAL)
                .WithMany(r => r.LessonsDAL)
                .HasForeignKey(p => p.LectorID)
                .HasConstraintName("FK_Lesson_User");

            modelBuilder.Entity<TrainingDAL>()
               .Property(p => p.LessonId)
               .IsRequired();
            modelBuilder.Entity<TrainingDAL>()
                .Property(p => p.StudentId)
                .IsRequired();
            modelBuilder.Entity<TrainingDAL>()
                 .HasOne(p => p.Lesson)
                 .WithMany(r => r.TrainingsDAL)
                 .HasForeignKey(p => p.LessonId)
                 .HasConstraintName("FK_Training_Lesson");
            modelBuilder.Entity<TrainingDAL>()
                .HasOne(p => p.Student)
                .WithMany(r => r.TrainingsDAL)
                .HasForeignKey(p => p.StudentId)
                .HasConstraintName("FK_Training_User");


            modelBuilder.Entity<TrainingDAL>()
                .Property(b => b.Grade)
                .HasDefaultValue(Score.Failure);

            modelBuilder.Entity<TrainingDAL>()
                .HasAlternateKey(c => new { c.LessonId, c.StudentId })
                .HasName("UK_Training");
        }
    }
}
