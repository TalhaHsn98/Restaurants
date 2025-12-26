using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain;

namespace TaskManagement.Infrastructure.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var task = modelBuilder.Entity<TaskItem>();

            task.ToTable("Tasks");

            task.Property(t => t.Id)
                .ValueGeneratedOnAdd(); ;

            task.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            task.Property(t => t.Description)
                .HasMaxLength(1000);

            task.Property(t => t.Priority)
                .HasConversion<int>();

            task.Property(t => t.Status)
                .HasConversion<int>();

            task.HasData(
       new TaskItem
       {
           Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
           Name = "Design API",
           Description = "Design REST endpoints",
           DueDate = DateTime.Today.AddDays(3),
           Priority = TaskPriority.High,
           Status = TaskManagement.Core.Domain.TaskStatus.New
       },
       new TaskItem
       {
           Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
           Name = "Implement Service Layer",
           Description = "Business rules & validation",
           DueDate = DateTime.Today.AddDays(5),
           Priority = TaskPriority.Medium,
           Status = TaskManagement.Core.Domain.TaskStatus.New
       },
       new TaskItem
       {
           Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
           Name = "Write Unit Tests",
           Description = "xUnit + Moq",
           DueDate = DateTime.Today.AddDays(7),
           Priority = TaskPriority.High,
           Status = TaskManagement.Core.Domain.TaskStatus.New
       },
       new TaskItem
       {
           Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
           Name = "Setup SQLite",
           Description = "EF Core + SQLite",
           DueDate = DateTime.Today.AddDays(2),
           Priority = TaskPriority.Low,
           Status = TaskManagement.Core.Domain.TaskStatus.InProgress
       },
       new TaskItem
       {
           Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
           Name = "Prepare README",
           Description = "How to run & test",
           DueDate = DateTime.Today.AddDays(1),
           Priority = TaskPriority.Medium,
           Status = TaskManagement.Core.Domain.TaskStatus.New
       }
   );
        }
    }
}
