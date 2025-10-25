using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexProjectPlanner.Core.Entities;

namespace PlexProjectPlanner.Core.Interfaces
{
    public interface IWorkflowRepository
    {
        Task<Workflow> GetByIdAsync(Guid id);
        Task<IEnumerable<Workflow>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<Workflow>> GetAllAsync();
        Task<Workflow> CreateAsync(Workflow workflow);
        Task<Workflow> UpdateAsync(Workflow workflow);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Workflow>> GetActiveWorkflowsAsync();
        Task<Workflow> GetDefaultWorkflowForProjectAsync(Guid projectId);
    }
}