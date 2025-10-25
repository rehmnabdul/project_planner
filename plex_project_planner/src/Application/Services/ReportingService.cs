using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlexProjectPlanner.Application.DTOs;
using PlexProjectPlanner.Application.Queries;
using PlexProjectPlanner.Core.Interfaces;

namespace PlexProjectPlanner.Application.Services
{
    public class ReportingService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ILoggingService _loggingService;

        public ReportingService(IProjectRepository projectRepository, 
            ITaskRepository taskRepository,
            ILoggingService loggingService)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _loggingService = loggingService;
        }

        public async Task<DashboardDTO> GetDashboardDataAsync(DashboardQuery query)
        {
            try
            {
                var dashboard = new DashboardDTO
                {
                    GeneratedAt = DateTime.UtcNow
                };

                // Get all projects for the user
                var projects = await _projectRepository.GetByUserIdAsync(query.UserId);
                dashboard.TotalProjects = projects.Count();
                
                dashboard.ActiveProjects = projects.Count(p => p.Status == Core.Entities.ProjectStatus.Active);
                dashboard.CompletedProjects = projects.Count(p => p.Status == Core.Entities.ProjectStatus.Completed);

                // Get all tasks related to user projects
                var projectIds = projects.Select(p => p.Id).ToList();
                var allTasks = new List<Core.Entities.Task>();

                foreach (var projectId in projectIds)
                {
                    var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
                    allTasks.AddRange(tasks);
                }

                dashboard.TotalTasks = allTasks.Count();
                dashboard.CompletedTasks = allTasks.Count(t => t.Status == Core.ValueObjects.TaskStatus.Done);
                dashboard.InProgressTasks = allTasks.Count(t => t.Status == Core.ValueObjects.TaskStatus.InProgress);

                // Calculate overdue tasks (tasks with due date in past and not completed)
                dashboard.OverdueTasks = allTasks.Count(t => 
                    t.DueDate.HasValue && 
                    t.DueDate.Value.Date < DateTime.Now.Date &&
                    t.Status != Core.ValueObjects.TaskStatus.Done);

                // Generate project summaries
                foreach (var project in projects)
                {
                    var projectTasks = allTasks.Where(t => t.ProjectId == project.Id).ToList();
                    var completedTasks = projectTasks.Count(t => t.Status == Core.ValueObjects.TaskStatus.Done);
                    
                    dashboard.ProjectSummaries.Add(new ProjectSummaryDTO
                    {
                        Id = project.Id,
                        Name = project.Name,
                        TotalTasks = projectTasks.Count,
                        CompletedTasks = completedTasks,
                        InProgressTasks = projectTasks.Count(t => t.Status == Core.ValueObjects.TaskStatus.InProgress),
                        CompletionPercentage = projectTasks.Any() ? (decimal)completedTasks / projectTasks.Count * 100 : 0,
                        CreatedAt = project.CreatedAt
                    });
                }

                // Generate task summaries for the dashboard
                foreach (var task in allTasks.Take(10)) // Just take first 10 for the summary
                {
                    dashboard.TaskSummaries.Add(new TaskSummaryDTO
                    {
                        Id = task.Id,
                        Title = task.Title,
                        ProjectName = projects.FirstOrDefault(p => p.Id == task.ProjectId)?.Name ?? "Unknown",
                        Status = task.Status.ToString(),
                        Priority = task.Priority.ToString(),
                        DueDate = task.DueDate,
                        Assignee = task.AssigneeId?.ToString() ?? "Unassigned",
                        CreatedAt = task.CreatedAt
                    });
                }

                await _loggingService.LogInfoAsync($"Dashboard data generated for user {query.UserId}", "ReportingService");
                return dashboard;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error generating dashboard data for user {query.UserId}: {ex.Message}", "ReportingService", ex);
                throw;
            }
        }

        public async Task<List<ProjectSummaryDTO>> GetProjectReportsAsync(Guid userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var projects = await _projectRepository.GetByUserIdAsync(userId);
                var allTasks = new List<Core.Entities.Task>();

                // Filter by date range if specified
                var filteredProjects = projects;
                if (startDate.HasValue && endDate.HasValue)
                {
                    filteredProjects = projects.Where(p => p.CreatedAt >= startDate.Value && p.CreatedAt <= endDate.Value.AddDays(1).AddSeconds(-1));
                }

                // Get all tasks for these projects
                foreach (var project in filteredProjects)
                {
                    var tasks = await _taskRepository.GetByProjectIdAsync(project.Id);
                    allTasks.AddRange(tasks);
                }

                var projectReports = new List<ProjectSummaryDTO>();
                foreach (var project in filteredProjects)
                {
                    var projectTasks = allTasks.Where(t => t.ProjectId == project.Id).ToList();
                    var completedTasks = projectTasks.Count(t => t.Status == Core.ValueObjects.TaskStatus.Done);

                    projectReports.Add(new ProjectSummaryDTO
                    {
                        Id = project.Id,
                        Name = project.Name,
                        TotalTasks = projectTasks.Count,
                        CompletedTasks = completedTasks,
                        InProgressTasks = projectTasks.Count(t => t.Status == Core.ValueObjects.TaskStatus.InProgress),
                        CompletionPercentage = projectTasks.Any() ? (decimal)completedTasks / projectTasks.Count * 100 : 0,
                        CreatedAt = project.CreatedAt
                    });
                }

                await _loggingService.LogInfoAsync($"Project reports generated for user {userId}", "ReportingService");
                return projectReports;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error generating project reports for user {userId}: {ex.Message}", "ReportingService", ex);
                throw;
            }
        }
    }
}