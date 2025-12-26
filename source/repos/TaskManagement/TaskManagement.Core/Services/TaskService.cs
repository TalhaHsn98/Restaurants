using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Contracts;
using TaskManagement.Core.Domain;
using TaskManagement.Core.Exceptions;

namespace TaskManagement.Core.Services
{
    public sealed class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly IDateTimeProvider _clock;
        private readonly IBusinessCalendar _calendar;

        public TaskService(ITaskRepository repo, IDateTimeProvider clock, IBusinessCalendar calendar)
        {
            _repo = repo;
            _clock = clock;
            _calendar = calendar;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            ValidateTask(task, isCreate: true);

            if (task.Id == Guid.Empty)
                task.Id = Guid.NewGuid();

            await EnforceHighPriorityCapAsync(task);

            await _repo.AddAsync(task);
            await _repo.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateTaskAsync(Guid id, TaskItem updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return null;

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.DueDate = updated.DueDate;
            existing.StartDate = updated.StartDate;
            existing.EndDate = updated.EndDate;
            existing.Priority = updated.Priority;
            existing.Status = updated.Status;

            ValidateTask(existing, isCreate: false);

            await EnforceHighPriorityCapAsync(existing, ignoreTaskId: existing.Id);

            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();
            return existing;
        }

        public Task<TaskItem?> GetTaskByIdAsync(Guid id) => _repo.GetByIdAsync(id);

        public Task<IReadOnlyList<TaskItem>> GetAllTasksAsync() => _repo.GetAllAsync();

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            await _repo.DeleteAsync(existing);
            await _repo.SaveChangesAsync();
            return true;
        }

        private void ValidateTask(TaskItem task, bool isCreate)
        {
            if (string.IsNullOrWhiteSpace(task.Name))
                throw new DomainValidationException("Task Name is required.");

            var due = task.DueDate.Date;
            var today = _clock.TodayLocal;

            if (due < today)
                throw new DomainValidationException("Due Date cannot be in the past.");

            if (!_calendar.IsBusinessDay(due))
                throw new DomainValidationException("Due Date cannot be on a weekend or holiday.");

            if (task.StartDate.HasValue && task.EndDate.HasValue && task.EndDate.Value < task.StartDate.Value)
                throw new DomainValidationException("End Date cannot be before Start Date.");

            if (task.Status == TaskManagement.Core.Domain.TaskStatus.Finished && !task.EndDate.HasValue)
                throw new DomainValidationException("Finished tasks must have an End Date.");
        }

        private async Task EnforceHighPriorityCapAsync(TaskItem task, Guid? ignoreTaskId = null)
        {
            if (task.Priority != TaskPriority.High) return;
            if (task.Status == TaskManagement.Core.Domain.TaskStatus.Finished) return;

            var count = await _repo.CountHighPriorityNotFinishedByDueDateAsync(task.DueDate.Date);

            if (ignoreTaskId.HasValue)
            {
                if (count > 100)
                    throw new DomainValidationException("Cannot exceed 100 High Priority tasks with the same due date that are not finished.");

                if (count == 100)
                    return;
            }

            if (count >= 100)
                throw new DomainValidationException("Cannot exceed 100 High Priority tasks with the same due date that are not finished.");
        }
    }
}
