using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pri.Ek2.Core.Dtos.RequestDtos;
using Pri.Ek2.Core.Dtos.ResponseDtos;
using Pri.Ek2.Core.Services.Interfaces;
using System.Security.Claims;

namespace Pri.Ek2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmissionController : ControllerBase
    {
        private readonly IEmissionGoalService _goalService;
        private readonly IEmissionReportService _reportService;

        public EmissionController(
            IEmissionGoalService goalService,
            IEmissionReportService reportService)
        {
            _goalService = goalService;
            _reportService = reportService;
        }

       
    }
}
