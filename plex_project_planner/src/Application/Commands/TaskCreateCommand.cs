using System;
using PlexProjectPlanner.Core.ValueObjects;

namespace PlexProjectPlanner.Application.Commands
{
    public class TaskCreateCommand
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? AssigneeId { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid CreatedBy { get; set; }
    }
}