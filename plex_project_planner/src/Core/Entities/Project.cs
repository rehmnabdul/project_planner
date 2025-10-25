using System;
using PlexProjectPlanner.Core.ValueObjects;

namespace PlexProjectPlanner.Core.Entities
{
    public class Project
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public ProjectSettings Settings { get; private set; }
        public ProjectStatus Status { get; private set; }

        // Private constructor for EF Core
        private Project() { }

        public Project(string name, Guid createdBy)
        {
            Id = Guid.NewGuid();
            SetName(name);
            CreatedBy = createdBy;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Settings = new ProjectSettings();
            Status = ProjectStatus.Active;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Project name is required", nameof(name));

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string? description)
        {
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetStatus(ProjectStatus status)
        {
            Status = status;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateSettings(ProjectSettings settings)
        {
            Settings = settings;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AssignToUser(Guid userId)
        {
            // Implementation for assigning project to users
            // This could involve adding to a collection of assigned users
            UpdatedAt = DateTime.UtcNow;
        }

        public void Archive()
        {
            Status = ProjectStatus.Archived;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            if (Status == ProjectStatus.Archived)
            {
                Status = ProjectStatus.Active;
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }

    public enum ProjectStatus
    {
        Active,
        Archived,
        Completed
    }
}