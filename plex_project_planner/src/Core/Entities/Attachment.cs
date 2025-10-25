using System;

namespace PlexProjectPlanner.Core.Entities
{
    public class Attachment
    {
        public Guid Id { get; private set; }
        public string FileName { get; private set; }
        public string ContentType { get; private set; }
        public long FileSize { get; private set; }
        public string StoragePath { get; private set; }
        public Guid? TaskId { get; private set; }
        public Guid? ProjectId { get; private set; }
        public Guid UploadedBy { get; private set; }
        public DateTime UploadedAt { get; private set; }

        // Private constructor for EF Core
        private Attachment() { }

        public Attachment(string fileName, string contentType, long fileSize, string storagePath, Guid uploadedBy, Guid? taskId = null, Guid? projectId = null)
        {
            Id = Guid.NewGuid();
            FileName = fileName ?? throw new ArgumentException("File name is required", nameof(fileName));
            ContentType = contentType ?? throw new ArgumentException("Content type is required", nameof(contentType));
            FileSize = fileSize;
            StoragePath = storagePath ?? throw new ArgumentException("Storage path is required", nameof(storagePath));
            
            if (taskId == null && projectId == null)
                throw new ArgumentException("Attachment must be associated with either a task or project");
                
            TaskId = taskId;
            ProjectId = projectId;
            UploadedBy = uploadedBy;
            UploadedAt = DateTime.UtcNow;
        }

        public void UpdateStoragePath(string newStoragePath)
        {
            if (string.IsNullOrWhiteSpace(newStoragePath))
                throw new ArgumentException("Storage path cannot be empty", nameof(newStoragePath));

            StoragePath = newStoragePath;
        }

        public void AssignToTask(Guid taskId)
        {
            TaskId = taskId;
            ProjectId = null; // Clear project assignment when assigning to task
        }

        public void AssignToProject(Guid projectId)
        {
            ProjectId = projectId;
            TaskId = null; // Clear task assignment when assigning to project
        }
    }
}