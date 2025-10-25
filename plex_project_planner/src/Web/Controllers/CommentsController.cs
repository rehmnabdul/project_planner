using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlexProjectPlanner.Application.Commands;
using PlexProjectPlanner.Core.DomainServices;
using PlexProjectPlanner.Core.Entities;
using PlexProjectPlanner.Application.DTOs;

namespace PlexProjectPlanner.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CollaborationService _collaborationService;

        public CommentsController(CollaborationService collaborationService)
        {
            _collaborationService = collaborationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(Guid id)
        {
            var comment = await _collaborationService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound();

            // For now, we'll return the entity directly, but in a real app
            // you'd want to convert it to a DTO
            return Ok(comment);
        }

        [HttpGet("task/{taskId}")]
        public async Task<IActionResult> GetCommentsByTask(Guid taskId)
        {
            // Implementation would retrieve comments by task ID
            // For now, returning a placeholder response
            var comments = new List<Comment>(); // Placeholder
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateCommand command)
        {
            try
            {
                var comment = await _collaborationService.CreateCommentAsync(
                    command.Content,
                    command.TaskId,
                    command.AuthorId);

                // For now, returning the entity directly, but in a real app
                // you'd want to convert it to a DTO
                return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] string content)
        {
            try
            {
                var comment = await _collaborationService.UpdateCommentAsync(id, content);

                // For now, returning the entity directly, but in a real app
                // you'd want to convert it to a DTO
                return Ok(comment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var result = await _collaborationService.DeleteCommentAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}