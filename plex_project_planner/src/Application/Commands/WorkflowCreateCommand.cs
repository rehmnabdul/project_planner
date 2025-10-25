using System;
using System.Collections.Generic;

namespace PlexProjectPlanner.Application.Commands
{
    public class WorkflowCreateCommand
    {
        public string Name { get; set; }
        public Guid ProjectId { get; set; }
        public List<string> InitialStatuses { get; set; } = new List<string> { "To Do", "In Progress", "Done" };
        public Guid CreatedBy { get; set; }
    }
}