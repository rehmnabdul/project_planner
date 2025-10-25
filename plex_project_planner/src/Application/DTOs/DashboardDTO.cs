using System;
using System.Collections.Generic;

namespace PlexProjectPlanner.Application.DTOs
{
    public class DashboardDTO
    {
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int CompletedProjects { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int OverdueTasks { get; set; }
        public int TotalUsers { get; set; }
        public List<ProjectSummaryDTO> ProjectSummaries { get; set; } = new List<ProjectSummaryDTO>();
        public List<TaskSummaryDTO> TaskSummaries { get; set; } = new List<TaskSummaryDTO>();
        public DateTime GeneratedAt { get; set; }
    }

    public class ProjectSummaryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int InProgressTasks { get; set; }
        public decimal CompletionPercentage { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TaskSummaryDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public string Assignee { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}