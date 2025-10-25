using System;

namespace PlexProjectPlanner.Core.Entities
{
    public class Comment
    {
        public Guid Id { get; private set; }
        public string Content { get; private set; }
        public Guid TaskId { get; private set; }
        public Guid AuthorId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsEdited { get; private set; }

        // Private constructor for EF Core
        private Comment() { }

        public Comment(string content, Guid taskId, Guid authorId)
        {
            Id = Guid.NewGuid();
            SetContent(content);
            TaskId = taskId;
            AuthorId = authorId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsEdited = false;
        }

        public void SetContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Comment content is required", nameof(content));

            Content = content;
            UpdatedAt = DateTime.UtcNow;
            IsEdited = true;
        }

        public void UpdateContent(string newContent)
        {
            SetContent(newContent);
        }
    }
}