using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using PlexProjectPlanner.Application.Commands;
using PlexProjectPlanner.Core.DomainServices;
using PlexProjectPlanner.Application.DTOs;

namespace PlexProjectPlanner.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkflowsController : ControllerBase
    {
        private readonly WorkflowService _workflowService;

        public WorkflowsController(WorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkflows([FromQuery] Guid projectId)
        {
            try
            {
                var workflows = await _workflowService.GetWorkflowsByProjectIdAsync(projectId);
                // Convert to DTOs and return
                return Ok(workflows); // Placeholder
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkflow(Guid id)
        {
            try
            {
                var workflow = await _workflowService.GetWorkflowByIdAsync(id);
                if (workflow == null)
                    return NotFound();

                // Convert to DTO and return
                return Ok(workflow); // Placeholder
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkflow([FromBody] WorkflowCreateCommand command)
        {
            try
            {
                var workflow = await _workflowService.CreateWorkflowAsync(
                    command.Name, 
                    command.ProjectId, 
                    command.CreatedBy, 
                    command.InitialStatuses);

                // Convert to DTO and return
                return CreatedAtAction(nameof(GetWorkflow), new { id = workflow.Id }, workflow); // Placeholder
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkflow(Guid id, [FromBody] object updateData)
        {
            try
            {
                // Implementation would update the workflow
                return Ok(new { message = "Update workflow endpoint - implementation pending" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkflow(Guid id)
        {
            try
            {
                var result = await _workflowService.DeleteWorkflowAsync(id);
                if (!result)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> AddStatusToWorkflow(Guid id, [FromBody] string status)
        {
            try
            {
                var workflow = await _workflowService.AddStatusToWorkflowAsync(id, status);
                return Ok(workflow); // Placeholder
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id}/transition")]
        public async Task<IActionResult> AddTransitionToWorkflow(Guid id, [FromBody] WorkflowTransitionRequest request)
        {
            try
            {
                var workflow = await _workflowService.AddTransitionToWorkflowAsync(id, request.FromStatus, request.ToStatus);
                return Ok(workflow); // Placeholder
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
    
    public class WorkflowTransitionRequest
    {
        public string FromStatus { get; set; }
        public string ToStatus { get; set; }
    }
}