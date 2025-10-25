using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexProjectPlanner.Core.Entities;

namespace PlexProjectPlanner.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetByIdAsync(Guid id);
        Task<IEnumerable<Comment>> GetByTaskIdAsync(Guid taskId);
        Task<IEnumerable<Comment>> GetByAuthorIdAsync(Guid authorId);
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}