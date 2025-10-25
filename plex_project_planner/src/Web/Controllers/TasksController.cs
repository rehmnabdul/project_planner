using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexProjectPlanner.Application.Commands;
using PlexProjectPlanner.Core.DomainServices;
using PlexProjectPlanner.Core.Entities;
using PlexProjectPlanner.Core.ValueObjects;
using PlexProjectPlanner.Application.DTOs;

namespace PlexProjectPlanner.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] string? query = null, 
                                                 [FromQuery] Guid? projectId = null, 
                                                 [FromQuery] Guid? assigneeId = null)
        {
            // Implementation would retrieve tasks based on the filters
            // For now, returning a placeholder response
            var tasks = new List<Task>(); // Placeholder
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            var taskDto = new TaskDTO(
                task.Id,
                task.Title,
                task.Description,
                task.ProjectId,
                task.AssigneeId,
                task.Priority,
                task.Status,
                task.DueDate,
                task.CreatedBy,
                task.CreatedAt,
                task.UpdatedAt,
                task.Position
            );

            return Ok(taskDto);
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetTasksByProject(Guid projectId, [FromQuery] string? status = null)
        {
            // Implementation would retrieve tasks by project ID and optional status
            // For now, returning a placeholder response
            var tasks = new List<Task>(); // Placeholder
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskCreateCommand command)
        {
            try
            {
                var task = await _taskService.CreateTaskAsync(
                    command.Title,
                    command.Description,
                    command.ProjectId,
                    command.AssigneeId,
                    command.Priority,
                    command.DueDate,
                    command.CreatedBy);

                var taskDto = new TaskDTO(
                    task.Id,
                    task.Title,
                    task.Description,
                    task.ProjectId,
                    task.AssigneeId,
                    task.Priority,
                    task.Status,
                    task.DueDate,
                    task.CreatedBy,
                    task.CreatedAt,
                    task.UpdatedAt,
                    task.Position
                );

                return CreatedAtAction(nameof(GetTask), new { id = task.Id }, taskDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskUpdateCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            try
            {
                var task = await _taskService.UpdateTaskAsync(
                    id,
                    command.Title,
                    command.Description,
                    command.AssigneeId,
                    command.Priority,
                    command.Status,
                    command.DueDate,
                    command.Position);

                var taskDto = new TaskDTO(
                    task.Id,
                    task.Title,
                    task.Description,
                    task.ProjectId,
                    task.AssigneeId,
                    task.Priority,
                    task.Status,
                    task.DueDate,
                    task.CreatedBy,
                    task.CreatedAt,
                    task.UpdatedAt,
                    task.Position
                );

                return Ok(taskDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(Guid id, [FromBody] TaskStatus status)
        {
            try
            {
                var task = await _taskService.UpdateTaskStatusAsync(id, status);

                var taskDto = new TaskDTO(
                    task.Id,
                    task.Title,
                    task.Description,
                    task.ProjectId,
                    task.AssigneeId,
                    task.Priority,
                    task.Status,
                    task.DueDate,
                    task.CreatedBy,
                    task.CreatedAt,
                    task.UpdatedAt,
                    task.Position
                );

                return Ok(taskDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/assign")]
        public async Task<IActionResult> AssignTask(Guid id, [FromBody] Guid? assigneeId)
        {
            try
            {
                var task = await _taskService.AssignTaskAsync(id, assigneeId);

                var taskDto = new TaskDTO(
                    task.Id,
                    task.Title,
                    task.Description,
                    task.ProjectId,
                    task.AssigneeId,
                    task.Priority,
                    task.Status,
                    task.DueDate,
                    task.CreatedBy,
                    task.CreatedAt,
                    task.UpdatedAt,
                    task.Position
                );

                return Ok(taskDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}