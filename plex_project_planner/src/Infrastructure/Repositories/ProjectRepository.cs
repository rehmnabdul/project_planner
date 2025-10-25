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
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetByIdAsync(Guid id)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> GetByNameAsync(string name, Guid createdBy)
        {
            return await _context.Projects
                .FirstOrDefaultAsync(p => p.Name == name && p.CreatedBy == createdBy);
        }

        public async Task<IEnumerable<Project>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Projects
                .Where(p => p.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .ToListAsync();
        }

        public async Task<Project> CreateAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project> UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Project>> GetActiveProjectsAsync()
        {
            return await _context.Projects
                .Where(p => p.Status == ProjectStatus.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetArchivedProjectsAsync()
        {
            return await _context.Projects
                .Where(p => p.Status == ProjectStatus.Archived)
                .ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetCompletedProjectsAsync()
        {
            return await _context.Projects
                .Where(p => p.Status == ProjectStatus.Completed)
                .ToListAsync();
        }
    }
}