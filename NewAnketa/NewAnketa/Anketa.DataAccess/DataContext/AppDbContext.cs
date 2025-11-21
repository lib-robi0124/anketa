using Anketa.DataAccess.Extensions;
using Anketa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Anketa.Domain.Enums;

namespace Anketa.DataAccess.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {  }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<QuestionForm> QuestionForms { get; set; }
        public DbSet<Answer> Answers { get; set; }
        // Note: AnswerSummary is not a DbSet - it's a DTO for reporting purposes only
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.SeedData();

           modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.QuestionForm)
                .WithMany(qf => qf.Answers)
                .HasForeignKey(a => a.QuestionFormId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Answer>()
                        .Property(a => a.CompanyId)
                        .IsRequired();
            modelBuilder.Entity<Answer>()
                        .Property(a => a.Age)
                        .IsRequired();
            modelBuilder.Entity<Answer>()
                        .Property(a => a.WorkExperience)
                        .IsRequired();
			modelBuilder.Entity<Answer>()
                        .Property(a => a.UserId)
                        .IsRequired();
            modelBuilder.Entity<Answer>()
                        .Property(a => a.QuestionId)
                        .IsRequired();
            modelBuilder.Entity<Answer>()
                        .Property(a => a.QuestionFormId)
                        .IsRequired();
			modelBuilder.Entity<Answer>()
                        .Property(a => a.ScaleValue)
						.IsRequired(false); // Nullable
            modelBuilder.Entity<Answer>()
                        .Property(a => a.TextValue)
						.IsRequired(false)
						.HasMaxLength(1000); // Nullable with max length
            modelBuilder.Entity<Answer>()
                        .Property(a => a.AnsweredDate)
						.IsRequired()
						.HasDefaultValueSql("GETUTCDATE()"); // Or appropriate SQL for your database
            modelBuilder.Entity<Answer>()
                .HasIndex(a => new { a.UserId, a.QuestionId, a.QuestionFormId })
                .IsUnique();
			modelBuilder.Entity<Answer>()
                .HasIndex(u => u.CompanyId);
            modelBuilder.Entity<Answer>()
                .HasIndex(u => u.UserId);
            modelBuilder.Entity<Answer>()
                .HasIndex(u => u.QuestionId);
            modelBuilder.Entity<Answer>()
                .HasIndex(u => u.QuestionFormId);
            modelBuilder.Entity<Answer>()
                .HasIndex(u => u.AnsweredDate);
            modelBuilder.Entity<Answer>()
                .HasIndex(u => u.Age);
            modelBuilder.Entity<Answer>()
                .HasIndex(u => u.WorkExperience);
            
			modelBuilder.Entity<Answer>()
                .HasIndex(a => new { a.CompanyId, a.AnsweredDate });
            modelBuilder.Entity<Answer>()
                .HasIndex(a => new { a.UserId, a.AnsweredDate });
            modelBuilder.Entity<Answer>()
                .HasIndex(a => new { a.QuestionFormId, a.AnsweredDate });


            modelBuilder.Entity<Role>()
                .Property(r => r.Type)
                .IsRequired()
                .HasConversion<int>();
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Role>()
                .Property(r => r.Description)
                .HasMaxLength(200);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.QuestionForm)
                .WithMany(f => f.Questions)
                .HasForeignKey(q => q.QuestionFormId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Question>()
                .HasOne(q => q.User)
                .WithMany()
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.QuestionType)
                .WithMany(qt => qt.Questions)
                .HasForeignKey(q => q.QuestionTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Answers)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .Property(u => u.CompanyId)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Sector)
                .IsRequired()
                .HasConversion<int>(); // Store enum as int
            modelBuilder.Entity<User>()
                .Property(u => u.Department)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Line)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);
            modelBuilder.Entity<User>()
                .Property(u => u.Gender)
                .IsRequired()
                .HasConversion<int>();
            modelBuilder.Entity<User>()
                .Property(u => u.PositionType)
                .IsRequired()
                .HasConversion<int>();
            modelBuilder.Entity<User>()
                .Property(u => u.EducationLevel)
                .IsRequired()
                .HasConversion<int>();
            modelBuilder.Entity<User>()
                .Property(u => u.Age)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.WorkExperience)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedDate)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.CompanyId);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Sector);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Department);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Line);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.RoleId);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Gender);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PositionType);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Age);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.WorkExperience);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.IsActive);

            modelBuilder.Entity<Question>()
                .Property(q => q.Text)
                .IsRequired()
                .HasMaxLength(500);
            modelBuilder.Entity<Question>()
                .Property(q => q.UserId)
                .IsRequired();
            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionTypeId)
                .IsRequired();
            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionFormId)
                .IsRequired();
            modelBuilder.Entity<Question>()
                .Property(q => q.IsRequired)
                .IsRequired()
                .HasDefaultValue(true);

            modelBuilder.Entity<QuestionType>()
                .Property(qt => qt.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<QuestionType>()
                .Property(qt => qt.Description)
                .HasMaxLength(200);

            modelBuilder.Entity<QuestionForm>()
                .Property(qf => qf.Title)
                .IsRequired()
                .HasMaxLength(200);
            modelBuilder.Entity<QuestionForm>()
                .Property(qf => qf.Description)
                .HasMaxLength(1000);
            modelBuilder.Entity<QuestionForm>()
                .Property(qf => qf.IsActive)
                .IsRequired()
                .HasDefaultValue(true);
            modelBuilder.Entity<QuestionForm>()
                .Property(qf => qf.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
