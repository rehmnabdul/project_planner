using System;
using PlexProjectPlanner.Core.ValueObjects;

namespace PlexProjectPlanner.Application.Commands
{
    public class ProjectUpdateCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ProjectStatus Status { get; set; }
    }
}