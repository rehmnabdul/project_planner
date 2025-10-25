using System;

namespace PlexProjectPlanner.Application.Queries
{
    public class DashboardQuery
    {
        public Guid UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IncludeArchived { get; set; } = false;
    }
}