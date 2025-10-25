using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskEntity = PlexProjectPlanner.Core.Entities.Task;
using PlexProjectPlanner.Core.Interfaces;
using PlexProjectPlanner.Infrastructure.Persistence;

namespace PlexProjectPlanner.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskEntity> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskEntity>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Tasks
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetByAssigneeIdAsync(Guid assigneeId)
        {
            return await _context.Tasks
                .Where(t => t.AssigneeId == assigneeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetByStatusAsync(string status)
        {
            // Convert string status to enum value
            if (Enum.TryParse<Core.ValueObjects.TaskStatus>(status, true, out var taskStatus))
            {
                return await _context.Tasks
                    .Where(t => t.Status.ToString() == taskStatus.ToString())
                    .ToListAsync();
            }
            return new List<TaskEntity>();
        }

        public async Task<IEnumerable<TaskEntity>> GetByUserIdAsync(Guid userId)
        {
            // Get tasks where the user is either the assignee or the creator
            return await _context.Tasks
                .Where(t => t.AssigneeId == userId || t.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<TaskEntity> CreateAsync(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity> UpdateAsync(TaskEntity task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Tasks.AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskEntity>> SearchTasksAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<TaskEntity>();

            return await _context.Tasks
                .Where(t => t.Title.Contains(query) || 
                           (t.Description != null && t.Description.Contains(query)))
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetByProjectAndStatusAsync(Guid projectId, string status)
        {
            // Convert string status to enum value
            if (Enum.TryParse<Core.ValueObjects.TaskStatus>(status, true, out var taskStatus))
            {
                return await _context.Tasks
                    .Where(t => t.ProjectId == projectId && t.Status.ToString() == taskStatus.ToString())
                    .ToListAsync();
            }
            return new List<TaskEntity>();
        }
    }
}