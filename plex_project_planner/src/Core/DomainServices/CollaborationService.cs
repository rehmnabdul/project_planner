using System;
using System.Threading.Tasks;
using PlexProjectPlanner.Core.Entities;
using PlexProjectPlanner.Core.Interfaces;

namespace PlexProjectPlanner.Core.DomainServices
{
    public class CollaborationService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILoggingService _loggingService;

        public CollaborationService(ICommentRepository commentRepository, 
            ITaskRepository taskRepository, 
            IProjectRepository projectRepository,
            ILoggingService loggingService)
        {
            _commentRepository = commentRepository;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _loggingService = loggingService;
        }

        public async Task<Comment> CreateCommentAsync(string content, Guid taskId, Guid authorId)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(content))
                    throw new ArgumentException("Comment content is required", nameof(content));

                // Verify task exists
                var task = await _taskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to create comment for non-existent task ID: {taskId}", "CollaborationService");
                    throw new InvalidOperationException($"Task with ID {taskId} does not exist.");
                }

                // Create new comment
                var comment = new Comment(content, taskId, authorId);

                // Save to repository
                var createdComment = await _commentRepository.CreateAsync(comment);

                await _loggingService.LogInfoAsync($"Comment created successfully with ID: {createdComment.Id}", "CollaborationService");
                return createdComment;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error creating comment for task ID {taskId}: {ex.Message}", "CollaborationService", ex);
                throw;
            }
        }

        public async Task<Comment> UpdateCommentAsync(Guid commentId, string newContent)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(newContent))
                    throw new ArgumentException("Comment content cannot be empty", nameof(newContent));

                // Get existing comment
                var comment = await _commentRepository.GetByIdAsync(commentId);
                if (comment == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to update non-existent comment with ID: {commentId}", "CollaborationService");
                    throw new InvalidOperationException($"Comment with ID {commentId} not found.");
                }

                // Update comment content
                comment.UpdateContent(newContent);

                // Save changes
                var updatedComment = await _commentRepository.UpdateAsync(comment);

                await _loggingService.LogInfoAsync($"Comment updated successfully with ID: {updatedComment.Id}", "CollaborationService");
                return updatedComment;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error updating comment with ID {commentId}: {ex.Message}", "CollaborationService", ex);
                throw;
            }
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            try
            {
                // Check if comment exists
                var comment = await _commentRepository.GetByIdAsync(commentId);
                if (comment == null)
                {
                    await _loggingService.LogWarningAsync($"Attempt to delete non-existent comment with ID: {commentId}", "CollaborationService");
                    return false;
                }

                // Delete the comment
                var result = await _commentRepository.DeleteAsync(commentId);

                if (result)
                    await _loggingService.LogInfoAsync($"Comment deleted successfully with ID: {commentId}", "CollaborationService");
                else
                    await _loggingService.LogWarningAsync($"Failed to delete comment with ID: {commentId}", "CollaborationService");

                return result;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error deleting comment with ID {commentId}: {ex.Message}", "CollaborationService", ex);
                throw;
            }
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            try
            {
                var comment = await _commentRepository.GetByIdAsync(commentId);
                
                if (comment != null)
                    await _loggingService.LogDebugAsync($"Retrieved comment with ID: {commentId}", "CollaborationService");
                else
                    await _loggingService.LogWarningAsync($"Attempt to retrieve non-existent comment with ID: {commentId}", "CollaborationService");

                return comment;
            }
            catch (Exception ex)
            {
                await _loggingService.LogErrorAsync($"Error retrieving comment with ID {commentId}: {ex.Message}", "CollaborationService", ex);
                throw;
            }
        }

        public async Task<Comment> AddMentionToCommentAsync(Guid commentId, Guid mentionedUserId)
        {
            // This would implement mention functionality
            // For now, we'll just return the comment
            return await _commentRepository.GetByIdAsync(commentId);
        }

        public async Task<bool> NotifyMentionedUsersAsync(Guid commentId)
        {
            // This would implement notification functionality
            // For now, returning true to indicate success
            return true;
        }
    }
}