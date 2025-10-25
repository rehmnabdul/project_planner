using System;

namespace PlexProjectPlanner.Application.Commands
{
    public class ProjectCreateCommand
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid CreatedBy { get; set; }
    }
}