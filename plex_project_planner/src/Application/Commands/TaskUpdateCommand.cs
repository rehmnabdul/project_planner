using System;
using PlexProjectPlanner.Core.ValueObjects;

namespace PlexProjectPlanner.Application.Commands
{
    public class TaskUpdateCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid? AssigneeId { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public int Position { get; set; }
    }
}