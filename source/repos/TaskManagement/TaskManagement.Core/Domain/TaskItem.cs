using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Domain
{
    public class TaskItem
    {
        public Guid Id { get; set; }                
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public DateTime DueDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
    }
}
