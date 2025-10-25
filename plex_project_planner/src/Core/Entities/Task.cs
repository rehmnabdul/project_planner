using System;
using PlexProjectPlanner.Core.ValueObjects;

namespace PlexProjectPlanner.Core.Entities
{
    public class Task
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public TaskPriority Priority { get; private set; }
        public TaskStatus Status { get; private set; }
        public DateTime? DueDate { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public int Position { get; private set; }

        // Private constructor for EF Core
        private Task() { }

        public Task(string title, Guid projectId, Guid createdBy)
        {
            Id = Guid.NewGuid();
            SetTitle(title);
            ProjectId = projectId;
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Priority = TaskPriority.Medium; // Default priority
            Status = TaskStatus.ToDo; // Default status
            Position = 0; // Default position
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Task title is required", nameof(title));

            Title = title;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string? description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAssignee(Guid? assigneeId)
        {
            AssigneeId = assigneeId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPriority(TaskPriority priority)
        {
            Priority = priority;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetStatus(TaskStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDueDate(DateTime? dueDate)
        {
            DueDate = dueDate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPosition(int position)
        {
            Position = position;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MoveToProject(Guid projectId)
        {
            ProjectId = projectId;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}