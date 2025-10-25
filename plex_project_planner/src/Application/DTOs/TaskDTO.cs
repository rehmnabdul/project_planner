using System;
using PlexProjectPlanner.Core.ValueObjects;

namespace PlexProjectPlanner.Application.DTOs
{
    public class TaskDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? AssigneeId { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Position { get; set; }

        public TaskDTO(Guid id, string title, string? description, Guid projectId, Guid? assigneeId,
            TaskPriority priority, TaskStatus status, DateTime? dueDate, Guid createdBy,
            DateTime createdAt, DateTime updatedAt, int position)
        {
            Id = id;
            Title = title;
            Description = description;
            ProjectId = projectId;
            AssigneeId = assigneeId;
            Priority = priority;
            Status = status;
            DueDate = dueDate;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Position = position;
        }
    }
}