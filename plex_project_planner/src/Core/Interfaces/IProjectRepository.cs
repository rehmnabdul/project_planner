using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexProjectPlanner.Core.Entities;

namespace PlexProjectPlanner.Core.Interfaces
{
    public interface IProjectRepository
    {
        Task<Project> GetByIdAsync(Guid id);
        Task<Project> GetByNameAsync(string name, Guid createdBy);
        Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> CreateAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Project>> GetActiveProjectsAsync();
        Task<IEnumerable<Project>> GetArchivedProjectsAsync();
        Task<IEnumerable<Project>> GetCompletedProjectsAsync();
    }
}