using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Domain;

namespace TaskManagement.Core.Contracts
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(TaskItem task);
        Task<TaskItem?> UpdateTaskAsync(Guid id, TaskItem updated);
        Task<TaskItem?> GetTaskByIdAsync(Guid id);
        Task<IReadOnlyList<TaskItem>> GetAllTasksAsync();
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
