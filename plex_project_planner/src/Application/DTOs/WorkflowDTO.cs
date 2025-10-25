using System;
using System.Collections.Generic;

namespace PlexProjectPlanner.Application.DTOs
{
    public class WorkflowDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public List<string> Statuses { get; set; }
        public Dictionary<string, List<string>> Transitions { get; set; }
        public bool IsActive { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public WorkflowDTO(Guid id, string name, Guid projectId, List<string> statuses, 
            Dictionary<string, List<string>> transitions, bool isActive, Guid createdBy, 
            DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            ProjectId = projectId;
            Statuses = statuses ?? new List<string>();
            Transitions = transitions ?? new Dictionary<string, List<string>>();
            IsActive = isActive;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}