using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskEntity = PlexProjectPlanner.Core.Entities.Task;
using PlexProjectPlanner.Core.Interfaces;
using PlexProjectPlanner.Core.Entities;

namespace PlexProjectPlanner.Application.Services
{
    public interface ISearchService
    {
        Task<SearchResult> SearchAsync(string query, Guid userId);
    }

    public class SearchService : ISearchService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILoggingService _loggingService;

        public SearchService(
            IProjectRepository projectRepository,
            ITaskRepository taskRepository,
            ICommentRepository commentRepository,
            ILoggingService loggingService)
        {
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
            _commentRepository = commentRepository;
            _loggingService = loggingService;
        }

        public async Task<SearchResult> SearchAsync(string query, Guid userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return new SearchResult();
                }

                var result = new SearchResult();
                
                // Search projects
                var allProjects = await _projectRepository.GetByUserIdAsync(userId);
                var projectMatches = allProjects.Where(p => 
                    p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    (p.Description?.Contains(query, StringComparison.OrdinalIgnoreCase) == true)
                );
                
                foreach (var project in projectMatches)
                {
                    result.Projects.Add(new SearchItem
                    {
                        Id = project.Id,
                        Title = project.Name,
                        Description = project.Description ?? "",
                        Type = "Project",
                        Url = $"/projects/{project.Id}"
                    });
                }

                // Search tasks
                var allTasks = new List<TaskEntity>();
                foreach (var projectId in allProjects.Select(p => p.Id))
                {
                    var tasks = await _taskRepository.GetByProjectIdAsync(projectId);
                    allTasks.AddRange(tasks);
                }

                var taskMatches = allTasks.Where(t => 
                    t.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    (t.Description?.Contains(query, StringComparison.OrdinalIgnoreCase) == true)
                );

                foreach (var task in taskMatches)
                {
                    var project = allProjects.FirstOrDefault(p => p.Id == task.ProjectId);
                    result.Tasks.Add(new SearchItem
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Description = task.Description ?? "",
                        Type = "Task",
                        Url = $"/tasks/{task.Id}",
                        AdditionalInfo = project?.Name
                    });
                }

                // Search comments
                var taskIds = allTasks.Select(t => t.Id).ToList();
                var allComments = new List<Comment>();
                
                foreach (var taskId in taskIds)
                {
                    var comments = await _commentRepository.GetByTaskIdAsync(taskId);
                    allComments.AddRange(comments);
                }

                var commentMatches = allComments.Where(c => 
                    c.Content.Contains(query, StringComparison.OrdinalIgnoreCase)
                );

                foreach (var comment in commentMatches)
                {
                    var task = allTasks.FirstOrDefault(t => t.Id == comment.TaskId);
                    var project = allProjects.FirstOrDefault(p => p.Id == task?.ProjectId);
                    
                    result.Comments.Add(new SearchItem
                    {
                        Id = comment.Id,
                        Title = $"Comment in {task?.Title ?? "Unknown Task"}",
                        Description = comment.Content.Length > 100 ? 
                            comment.Content.Substring(0, 100) + "..." : comment.Content,
                        Type = "Comment",
                        Url = $"/tasks/{task?.Id}#comment-{comment.Id}",
                        AdditionalInfo = project?.Name
                    });
                }

                await _loggingService.LogInfoAsync($"Search executed for user {userId} with query '{query}', found {result.TotalResults} results", "SearchService");
                return result;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error during search for user {userId} with query '{query}': {ex.Message}", "SearchService", ex);
                throw;
            }
        }
    }

    public class SearchResult
    {
        public List<SearchItem> Projects { get; set; } = new List<SearchItem>();
        public List<SearchItem> Tasks { get; set; } = new List<SearchItem>();
        public List<SearchItem> Comments { get; set; } = new List<SearchItem>();
        
        public int TotalResults => Projects.Count + Tasks.Count + Comments.Count;
    }

    public class SearchItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string AdditionalInfo { get; set; }
    }
}