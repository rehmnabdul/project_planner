using System;

namespace PlexProjectPlanner.Application.DTOs
{
    public class AttachmentDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public string StoragePath { get; set; }
        public Guid? TaskId { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid UploadedBy { get; set; }
        public DateTime UploadedAt { get; set; }

        public AttachmentDTO(Guid id, string fileName, string contentType, long fileSize,
            string storagePath, Guid? taskId, Guid? projectId, Guid uploadedBy, DateTime uploadedAt)
        {
            Id = id;
            FileName = fileName;
            ContentType = contentType;
            FileSize = fileSize;
            StoragePath = storagePath;
            TaskId = taskId;
            ProjectId = projectId;
            UploadedBy = uploadedBy;
            UploadedAt = uploadedAt;
        }
    }
}