using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexProjectPlanner.Core.Entities;

namespace PlexProjectPlanner.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<Task> GetByIdAsync(Guid id);
        Task<IEnumerable<Task>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<Task>> GetByAssigneeIdAsync(Guid assigneeId);
        Task<IEnumerable<Task>> GetByStatusAsync(string status);
        Task<IEnumerable<Task>> GetByUserIdAsync(Guid userId);
        Task<Task> CreateAsync(Task task);
        Task<Task> UpdateAsync(Task task);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Task>> SearchTasksAsync(string query);
        Task<IEnumerable<Task>> GetByProjectAndStatusAsync(Guid projectId, string status);
    }
}