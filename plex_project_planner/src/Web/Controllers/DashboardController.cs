using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using PlexProjectPlanner.Application.Queries;
using PlexProjectPlanner.Application.Services;
using PlexProjectPlanner.Application.DTOs;

namespace PlexProjectPlanner.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ReportingService _reportingService;

        public DashboardController(ReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData([FromQuery] Guid userId, [FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null, [FromQuery] bool includeArchived = false)
        {
            try
            {
                var query = new DashboardQuery
                {
                    UserId = userId,
                    FromDate = fromDate,
                    ToDate = toDate,
                    IncludeArchived = includeArchived
                };

                var dashboardData = await _reportingService.GetDashboardDataAsync(query);
                return Ok(dashboardData);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("projects")]
        public async Task<IActionResult> GetProjectReports([FromQuery] Guid userId, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                var projectReports = await _reportingService.GetProjectReportsAsync(userId, startDate, endDate);
                return Ok(projectReports);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user-productivity")]
        public async Task<IActionResult> GetUserProductivity([FromQuery] Guid userId, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            // Placeholder implementation - would return user productivity metrics
            return Ok(new { 
                message = "User productivity metrics endpoint - implementation pending",
                userId = userId,
                startDate = startDate,
                endDate = endDate 
            });
        }

        [HttpGet("project-timeline")]
        public async Task<IActionResult> GetProjectTimeline([FromQuery] Guid projectId)
        {
            // Placeholder implementation - would return project timeline data
            return Ok(new { 
                message = "Project timeline endpoint - implementation pending",
                projectId = projectId 
            });
        }
    }
}