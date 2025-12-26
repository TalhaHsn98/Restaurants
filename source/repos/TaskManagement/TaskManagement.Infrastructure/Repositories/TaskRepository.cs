using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Contracts;
using TaskManagement.Core.Domain;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public Task<TaskItem?> GetByIdAsync(Guid id) =>
            _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        public async Task<IReadOnlyList<TaskItem>> GetAllAsync() =>
            await _context.Tasks.AsNoTracking().ToListAsync();

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            return Task.CompletedTask;
        }

        public Task<int> CountHighPriorityNotFinishedByDueDateAsync(DateTime dueDate)
        {
            return _context.Tasks.CountAsync(t =>
                t.DueDate.Date == dueDate.Date &&
                t.Priority == TaskPriority.High &&
                t.Status != TaskManagement.Core.Domain.TaskStatus.Finished);
        }

        public Task SaveChangesAsync() =>
            _context.SaveChangesAsync();
    }
}
