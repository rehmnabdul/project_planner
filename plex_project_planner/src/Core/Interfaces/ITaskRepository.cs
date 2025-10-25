using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskEntity = PlexProjectPlanner.Core.Entities.Task;

namespace PlexProjectPlanner.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskEntity>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskEntity>> GetByAssigneeIdAsync(Guid assigneeId);
        Task<IEnumerable<TaskEntity>> GetByStatusAsync(string status);
        Task<IEnumerable<TaskEntity>> GetByUserIdAsync(Guid userId);
        Task<TaskEntity> CreateAsync(TaskEntity task);
        Task<TaskEntity> UpdateAsync(TaskEntity task);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<TaskEntity>> SearchTasksAsync(string query);
        Task<IEnumerable<TaskEntity>> GetByProjectAndStatusAsync(Guid projectId, string status);
    }
}