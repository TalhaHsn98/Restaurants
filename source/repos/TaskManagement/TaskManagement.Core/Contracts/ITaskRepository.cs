using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain;

namespace TaskManagement.Core.Contracts
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<TaskItem>> GetAllAsync();
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);

        Task<int> CountHighPriorityNotFinishedByDueDateAsync(DateTime dueDate);
        Task SaveChangesAsync();
    }
}
