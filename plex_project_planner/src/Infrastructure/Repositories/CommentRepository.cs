using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlexProjectPlanner.Core.Entities;
using PlexProjectPlanner.Core.Interfaces;
using PlexProjectPlanner.Infrastructure.Persistence;

namespace PlexProjectPlanner.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetByTaskIdAsync(Guid taskId)
        {
            return await _context.Comments
                .Where(c => c.TaskId == taskId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetByAuthorIdAsync(Guid authorId)
        {
            return await _context.Comments
                .Where(c => c.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            return await _context.Comments
                .ToListAsync();
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Comments.AnyAsync(c => c.Id == id);
        }
    }
}