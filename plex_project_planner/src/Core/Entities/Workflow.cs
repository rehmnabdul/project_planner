using System;
using System.Collections.Generic;

namespace PlexProjectPlanner.Core.Entities
{
    public class Workflow
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid ProjectId { get; private set; }
        public List<string> Statuses { get; private set; }
        public Dictionary<string, List<string>> Transitions { get; private set; } // FromStatus -> ToStatus
        public bool IsActive { get; private set; }
        public Guid CreatedBy { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Private constructor for EF Core
        private Workflow() { }

        public Workflow(string name, Guid projectId, Guid createdBy)
        {
            Id = Guid.NewGuid();
            SetName(name);
            ProjectId = projectId;
            CreatedBy = createdBy;
            Statuses = new List<string> { "To Do", "In Progress", "Done" }; // Default statuses
            Transitions = new Dictionary<string, List<string>>();
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            // Set default transitions
            Transitions["To Do"] = new List<string> { "In Progress" };
            Transitions["In Progress"] = new List<string> { "To Do", "Done" };
            Transitions["Done"] = new List<string> { "In Progress" };
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Workflow name is required", nameof(name));

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status name cannot be empty", nameof(status));

            if (!Statuses.Contains(status))
            {
                Statuses.Add(status);
                // Initialize transitions for this new status
                if (!Transitions.ContainsKey(status))
                    Transitions[status] = new List<string>();
            }
        }

        public void RemoveStatus(string status)
        {
            // Don't allow removing default statuses that are essential
            if (status != "To Do" && status != "In Progress" && status != "Done")
            {
                Statuses.Remove(status);
                // Remove transitions to/from this status
                Transitions.Remove(status);
                foreach (var key in Transitions.Keys)
                {
                    Transitions[key].RemoveAll(s => s == status);
                }
            }
        }

        public void AddTransition(string fromStatus, string toStatus)
        {
            if (string.IsNullOrWhiteSpace(fromStatus) || string.IsNullOrWhiteSpace(toStatus))
                throw new ArgumentException("From and To statuses cannot be empty");

            if (!Statuses.Contains(fromStatus) || !Statuses.Contains(toStatus))
                throw new ArgumentException("Both from and to statuses must exist in the workflow");

            if (!Transitions.ContainsKey(fromStatus))
                Transitions[fromStatus] = new List<string>();

            if (!Transitions[fromStatus].Contains(toStatus))
                Transitions[fromStatus].Add(toStatus);
        }

        public void RemoveTransition(string fromStatus, string toStatus)
        {
            if (Transitions.ContainsKey(fromStatus))
            {
                Transitions[fromStatus].RemoveAll(s => s == toStatus);
            }
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool CanTransition(string fromStatus, string toStatus)
        {
            return Transitions.ContainsKey(fromStatus) && Transitions[fromStatus].Contains(toStatus);
        }
    }
}