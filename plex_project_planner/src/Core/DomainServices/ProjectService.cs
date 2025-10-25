using System;
using System.Threading.Tasks;
using PlexProjectPlanner.Core.Entities;
using PlexProjectPlanner.Core.Interfaces;
using PlexProjectPlanner.Core.ValueObjects;

namespace PlexProjectPlanner.Core.DomainServices
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILoggingService _loggingService;

        public ProjectService(IProjectRepository projectRepository, ILoggingService loggingService)
        {
            _projectRepository = projectRepository;
            _loggingService = loggingService;
        }

        public async Task<Project> CreateProjectAsync(string name, string? description, Guid createdBy)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Project name is required", nameof(name));

                // Check if project with same name already exists for this user
                var existingProject = await _projectRepository.GetByNameAsync(name, createdBy);
                if (existingProject != null)
                    throw new InvalidOperationException($"A project with name '{name}' already exists for this user.");

                // Create new project
                var project = new Project(name, createdBy);
                if (!string.IsNullOrEmpty(description))
                    project.SetDescription(description);

                // Save to repository
                var createdProject = await _projectRepository.CreateAsync(project);

                await _loggingService.LogInfoAsync($"Project created successfully with ID: {createdProject.Id}", "ProjectService");
                return createdProject;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error creating project: {ex.Message}", "ProjectService", ex);
                throw;
            }
        }

        public async Task<Project> UpdateProjectAsync(Guid projectId, string name, string? description, ProjectStatus status)
        {
            try
            {
                // Get existing project
                var project = await _projectRepository.GetByIdAsync(projectId);
                if (project == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to update non-existent project with ID: {projectId}", "ProjectService");
                    throw new InvalidOperationException($"Project with ID {projectId} not found.");
                }

                // Update project properties
                project.SetName(name);
                project.SetDescription(description);
                project.SetStatus(status);

                // Save changes
                var updatedProject = await _projectRepository.UpdateAsync(project);

                await _loggingService.LogInfoAsync($"Project updated successfully with ID: {updatedProject.Id}", "ProjectService");
                return updatedProject;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error updating project with ID {projectId}: {ex.Message}", "ProjectService", ex);
                throw;
            }
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            try
            {
                // Check if project exists
                var project = await _projectRepository.GetByIdAsync(projectId);
                if (project == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to delete non-existent project with ID: {projectId}", "ProjectService");
                    return false;
                }

                // Delete the project
                var result = await _projectRepository.DeleteAsync(projectId);

                if (result)
                    await _loggingService.LogInfoAsync($"Project deleted successfully with ID: {projectId}", "ProjectService");
                else
                    await _loggingService.LogWarningAsync($"Failed to delete project with ID: {projectId}", "ProjectService");

                return result;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error deleting project with ID {projectId}: {ex.Message}", "ProjectService", ex);
                throw;
            }
        }

        public async Task<Project> GetProjectByIdAsync(Guid projectId)
        {
            return await _projectRepository.GetByIdAsync(projectId);
        }

        public async Task<Project> ArchiveProjectAsync(Guid projectId)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(projectId);
                if (project == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to archive non-existent project with ID: {projectId}", "ProjectService");
                    throw new InvalidOperationException($"Project with ID {projectId} not found.");
                }

                project.Archive();
                var updatedProject = await _projectRepository.UpdateAsync(project);

                await _loggingService.LogInfoAsync($"Project archived successfully with ID: {projectId}", "ProjectService");
                return updatedProject;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error archiving project with ID {projectId}: {ex.Message}", "ProjectService", ex);
                throw;
            }
        }

        public async Task<Project> RestoreProjectAsync(Guid projectId)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(projectId);
                if (project == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to restore non-existent project with ID: {projectId}", "ProjectService");
                    throw new InvalidOperationException($"Project with ID {projectId} not found.");
                }

                project.Restore();
                var updatedProject = await _projectRepository.UpdateAsync(project);

                await _loggingService.LogInfoAsync($"Project restored successfully with ID: {projectId}", "ProjectService");
                return updatedProject;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error restoring project with ID {projectId}: {ex.Message}", "ProjectService", ex);
                throw;
            }
        }

        public async Task<Project> UpdateProjectSettingsAsync(Guid projectId, ProjectSettings settings)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(projectId);
                if (project == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to update settings of non-existent project with ID: {projectId}", "ProjectService");
                    throw new InvalidOperationException($"Project with ID {projectId} not found.");
                }

                project.UpdateSettings(settings);
                var updatedProject = await _projectRepository.UpdateAsync(project);

                await _loggingService.LogInfoAsync($"Project settings updated successfully for project ID: {projectId}", "ProjectService");
                return updatedProject;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error updating project settings for project ID {projectId}: {ex.Message}", "ProjectService", ex);
                throw;
            }
        }
    }
}