using System;

namespace PlexProjectPlanner.Application.Commands
{
    public class CommentCreateCommand
    {
        public string Content { get; set; }
        public Guid TaskId { get; set; }
        public Guid AuthorId { get; set; }
    }
}