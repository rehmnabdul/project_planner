using System;
using ProjectStatus = PlexProjectPlanner.Core.Entities.ProjectStatus;
using ProjectSettings = PlexProjectPlanner.Core.ValueObjects.ProjectSettings;

namespace PlexProjectPlanner.Application.DTOs
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProjectSettings Settings { get; set; }
        public ProjectStatus Status { get; set; }

        public ProjectDTO(Guid id, string name, string? description, Guid createdBy, 
            DateTime createdAt, DateTime updatedAt, ProjectSettings settings, ProjectStatus status)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Settings = settings;
            Status = status;
        }
    }
}