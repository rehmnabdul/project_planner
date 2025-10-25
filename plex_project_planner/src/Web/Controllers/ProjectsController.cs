using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using PlexProjectPlanner.Application.Commands;
using PlexProjectPlanner.Core.DomainServices;
using PlexProjectPlanner.Core.Entities;
using PlexProjectPlanner.Application.DTOs;

namespace PlexProjectPlanner.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            // Implementation would retrieve projects for the current user
            // For now, returning a placeholder response
            return Ok(new List<Project>()); // Placeholder
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound();

            // Convert to DTO and return
            var projectDto = new ProjectDTO(
                project.Id,
                project.Name,
                project.Description,
                project.CreatedBy,
                project.CreatedAt,
                project.UpdatedAt,
                project.Settings,
                project.Status
            );
            
            return Ok(projectDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectCreateCommand command)
        {
            try
            {
                var project = await _projectService.CreateProjectAsync(
                    command.Name, 
                    command.Description, 
                    command.CreatedBy);
                
                var projectDto = new ProjectDTO(
                    project.Id,
                    project.Name,
                    project.Description,
                    project.CreatedBy,
                    project.CreatedAt,
                    project.UpdatedAt,
                    project.Settings,
                    project.Status
                );
                
                return CreatedAtAction(nameof(GetProject), new { id = project.Id }, projectDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectUpdateCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            try
            {
                var project = await _projectService.UpdateProjectAsync(
                    id,
                    command.Name,
                    command.Description,
                    command.Status);

                var projectDto = new ProjectDTO(
                    project.Id,
                    project.Name,
                    project.Description,
                    project.CreatedBy,
                    project.CreatedAt,
                    project.UpdatedAt,
                    project.Settings,
                    project.Status
                );
                
                return Ok(projectDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/archive")]
        public async Task<IActionResult> ArchiveProject(Guid id)
        {
            try
            {
                var project = await _projectService.ArchiveProjectAsync(id);
                var projectDto = new ProjectDTO(
                    project.Id,
                    project.Name,
                    project.Description,
                    project.CreatedBy,
                    project.CreatedAt,
                    project.UpdatedAt,
                    project.Settings,
                    project.Status
                );
                
                return Ok(projectDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/restore")]
        public async Task<IActionResult> RestoreProject(Guid id)
        {
            try
            {
                var project = await _projectService.RestoreProjectAsync(id);
                var projectDto = new ProjectDTO(
                    project.Id,
                    project.Name,
                    project.Description,
                    project.CreatedBy,
                    project.CreatedAt,
                    project.UpdatedAt,
                    project.Settings,
                    project.Status
                );
                
                return Ok(projectDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}