using System;
using System.Threading.Tasks;
using TaskEntity = PlexProjectPlanner.Core.Entities.Task;
using PlexProjectPlanner.Core.Interfaces;
using PlexProjectPlanner.Core.ValueObjects;

namespace PlexProjectPlanner.Core.DomainServices
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILoggingService _loggingService;

        public TaskService(ITaskRepository taskRepository, IProjectRepository projectRepository, ILoggingService loggingService)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _loggingService = loggingService;
        }

        public async Task<TaskEntity> CreateTaskAsync(string title, string? description, Guid projectId, 
            Guid? assigneeId, TaskPriority priority, DateTime? dueDate, Guid createdBy)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Task title is required", nameof(title));

                // Verify project exists
                var project = await _projectRepository.GetByIdAsync(projectId);
                if (project == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to create task for non-existent project ID: {projectId}", "TaskService");
                    throw new InvalidOperationException($"Project with ID {projectId} does not exist.");
                }

                // Create new task
                var task = new TaskEntity(title, projectId, createdBy);
                task.SetDescription(description);
                task.SetAssignee(assigneeId);
                task.SetPriority(priority);
                task.SetDueDate(dueDate);

                // Save to repository
                var createdTask = await _taskRepository.CreateAsync(task);

                await _loggingService.LogInfoAsync($"Task created successfully with ID: {createdTask.Id}", "TaskService");
                return createdTask;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error creating task: {ex.Message}", "TaskService", ex);
                throw;
            }
        }

        public async Task<TaskEntity> UpdateTaskAsync(Guid taskId, string title, string? description, 
            Guid? assigneeId, TaskPriority priority, Core.ValueObjects.TaskStatus status, DateTime? dueDate, int position)
        {
            try
            {
                // Get existing task
                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to update non-existent task with ID: {taskId}", "TaskService");
                    throw new InvalidOperationException($"Task with ID {taskId} not found.");
                }

                // Update task properties
                task.SetTitle(title);
                task.SetDescription(description);
                task.SetAssignee(assigneeId);
                task.SetPriority(priority);
                task.SetStatus(status);
                task.SetDueDate(dueDate);
                task.SetPosition(position);

                // Save changes
                var updatedTask = await _taskRepository.UpdateAsync(task);

                await _loggingService.LogInfoAsync($"Task updated successfully with ID: {updatedTask.Id}", "TaskService");
                return updatedTask;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error updating task with ID {taskId}: {ex.Message}", "TaskService", ex);
                throw;
            }
        }

        public async Task<bool> DeleteTaskAsync(Guid taskId)
        {
            try
            {
                // Check if task exists
                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to delete non-existent task with ID: {taskId}", "TaskService");
                    return false;
                }

                // Delete the task
                var result = await _taskRepository.DeleteAsync(taskId);

                if (result)
                    await _loggingService.LogInfoAsync($"Task deleted successfully with ID: {taskId}", "TaskService");
                else
                    await _loggingService.LogWarningAsync($"Failed to delete task with ID: {taskId}", "TaskService");

                return result;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error deleting task with ID {taskId}: {ex.Message}", "TaskService", ex);
                throw;
            }
        }

        public async Task<TaskEntity> GetTaskByIdAsync(Guid taskId)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(taskId);
                
                if (task != null)
                    await _loggingService.LogDebugAsync($"Retrieved task with ID: {taskId}", "TaskService");
                else
                    await _loggingService.LogWarningAsync($"Attempt to retrieve non-existent task with ID: {taskId}", "TaskService");

                return task;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error retrieving task with ID {taskId}: {ex.Message}", "TaskService", ex);
                throw;
            }
        }

        public async Task<TaskEntity> UpdateTaskStatusAsync(Guid taskId, Core.ValueObjects.TaskStatus newStatus)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to update status of non-existent task with ID: {taskId}", "TaskService");
                    throw new InvalidOperationException($"Task with ID {taskId} not found.");
                }

                task.SetStatus(newStatus);
                var updatedTask = await _taskRepository.UpdateAsync(task);

                await _loggingService.LogInfoAsync($"Task status updated to {newStatus} for task ID: {taskId}", "TaskService");
                return updatedTask;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error updating task status for task ID {taskId}: {ex.Message}", "TaskService", ex);
                throw;
            }
        }

        public async Task<TaskEntity> AssignTaskAsync(Guid taskId, Guid? assigneeId)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to assign non-existent task with ID: {taskId}", "TaskService");
                    throw new InvalidOperationException($"Task with ID {taskId} not found.");
                }

                task.SetAssignee(assigneeId);
                var updatedTask = await _taskRepository.UpdateAsync(task);

                if (assigneeId.HasValue)
                    await _loggingService.LogInfoAsync($"Task assigned to user {assigneeId.Value} with ID: {taskId}", "TaskService");
                else
                    await _loggingService.LogInfoAsync($"Task unassigned with ID: {taskId}", "TaskService");

                return updatedTask;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error assigning task with ID {taskId}: {ex.Message}", "TaskService", ex);
                throw;
            }
        }

        public async Task<TaskEntity> MoveTaskToProjectAsync(Guid taskId, Guid newProjectId)
        {
            try
            {
                // Verify new project exists
                var project = await _projectRepository.GetByIdAsync(newProjectId);
                if (project == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to move task to non-existent project ID: {newProjectId}", "TaskService");
                    throw new InvalidOperationException($"Project with ID {newProjectId} does not exist.");
                }

                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to move non-existent task with ID: {taskId}", "TaskService");
                    throw new InvalidOperationException($"Task with ID {taskId} not found.");
                }

                task.MoveToProject(newProjectId);
                var updatedTask = await _taskRepository.UpdateAsync(task);

                await _loggingService.LogInfoAsync($"Task moved from project to project {newProjectId} with ID: {taskId}", "TaskService");
                return updatedTask;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error moving task with ID {taskId} to project {newProjectId}: {ex.Message}", "TaskService", ex);
                throw;
            }
        }
    }
}