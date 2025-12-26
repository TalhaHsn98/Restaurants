using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.API.DTOs;
using TaskManagement.Core.Contracts;
using TaskManagement.Core.Domain;
using TaskManagement.Core.Exceptions;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TaskItem>>> GetAll()
        {
            var tasks = await _service.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItem>> GetById(Guid id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            return task is null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create([FromBody] CreateTaskRequest request)
        {
            try
            {
                var entity = new TaskItem
                {
                    Name = request.Name,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,

                    Priority = request.Priority,
                    Status = request.Status
                };

                var created = await _service.CreateTaskAsync(entity);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TaskItem>> Update(Guid id, [FromBody] UpdateTaskRequest request)
        {
            try
            {
                var updated = new TaskItem
                {
                    Name = request.Name,
                    Description = request.Description,
                    DueDate = request.DueDate,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Priority = request.Priority,
                    Status = request.Status
                };

                var result = await _service.UpdateTaskAsync(id, updated);
                return result is null ? NotFound() : Ok(result);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteTaskAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
